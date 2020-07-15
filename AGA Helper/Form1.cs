using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.IO.Compression;
using Microsoft.VisualBasic.FileIO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using AssetsTools.NET;
using AssetsTools.NET.Extra;
using AssetsTools.NET.Extra.Decompressors;
using Ookii.Dialogs;
using MediaDevices;


namespace AGA_Helper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            groupBox_source.AllowDrop = true;
            groupBox_target.AllowDrop = true;
            
            if (File.Exists(iniPath))
            {
                settings = js.Deserialize<Settings>(File.ReadAllText(iniPath));
            }
            else
            {
                settings.dataPath = "";
                settings.strippedDataPath = "";
                settings.activeTab = 0;
                File.WriteAllText(iniPath, js.Serialize(settings).Replace(",", ",\n\t").Replace("{", "{\n\t").Replace("}", "\n}"));
            }
            if (settings.dataPath == "")
            {
                dataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "Low\\COLOPL, Inc_\\アリス・ギア・アイギス";
                if (!Directory.Exists(dataPath))
                {
                    dataPath = "";
                }
            }
            else
            {
                dataPath = settings.dataPath;
            }
            /*if (settings.strippedDataPath == "")
            {
                strippedDataPath = AppDomain.CurrentDomain.BaseDirectory + @"_nocrc";
            }
            else
            {
                strippedDataPath = settings.strippedDataPath;
            }*/
            strippedDataPath = settings.strippedDataPath;
            if (!strippedDataPath.Contains(@":\") && strippedDataPath != "")
            {
                strippedDataPath = AppDomain.CurrentDomain.BaseDirectory + strippedDataPath;
            }
            
            tabControl1.SelectTab(settings.activeTab);
            label_dataPath.Text = dataPath;
            label_strippedDataPath.Text = strippedDataPath;



            gamePath = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\DMM GAMES\\Launcher\\Content\\alicegearaegisexe", "Path", "");
            if (!File.Exists(gamePath + @"\alice_main.exe"))
            {
                gamePath = AppDomain.CurrentDomain.BaseDirectory;
            }

            label_gamepath.Text = gamePath;
            
            assemblyPath = gamePath + "\\alice_main_Data\\Managed\\Assembly-CSharp.dll";
            enginePath = gamePath + "\\alice_main_Data\\Managed\\UnityEngine.CoreModule.dll";

            nocrcPath = strippedDataPath;
            crcPath = strippedDataPath + @"\_crc\";
            //nocrcPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data PC", "_nocrc");
            //crcPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data PC", "_crc");

            label_dataPath.Text = dataPath;


            if(!File.Exists(assemblyPath) || !File.Exists(enginePath))
            {
                button_patch.Enabled = false;
                button_restore.Enabled = false;
                button_patchTranslation.Enabled = false;
                button_updateTranslation.Enabled = false;
                button_permaLock.Enabled = false;
                button_deobfuscate.Enabled = false;
                button_obfuscate.Enabled = false;
                checkBox_lock_a.Enabled = false;
                checkBox_lock_u.Enabled = false;
                button_launch.Enabled = false;
            }
            else
            {
                string md5 = CalculateMD5(enginePath);
                if (md5 == md5Patched)
                {
                    button_patchTranslation.Text = "Patched";
                    button_patchTranslation.ForeColor = System.Drawing.Color.Green;
                    button_updateTranslation.Enabled = true;
                }
                else
                {
                    button_patchTranslation.Text = "Patch";
                    button_patchTranslation.ForeColor = System.Drawing.Color.Black;
                    button_updateTranslation.Enabled = false;
                }

                button_patch.Text = "Uncensor";
                button_patch.ForeColor = System.Drawing.Color.Black;

                /*
                ascs = File.ReadAllBytes(assemblyPath);

                if (GetPosition(ascs, pattern, 0) >= 0)
                {
                    button_patch.Text = "Uncensor";
                    button_restore.Text = "Censored";
                    button_patch.ForeColor = System.Drawing.Color.Black;
                    button_restore.ForeColor = System.Drawing.Color.Red;
                }
                else if (GetPosition(ascs, patternUncensored, 0) >= 0)
                {
                    button_patch.Text = "Uncensored";
                    button_restore.Text = "Censor";
                    button_patch.ForeColor = System.Drawing.Color.Green;
                    button_restore.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    button_patch.Enabled = false;
                    button_restore.Enabled = false;
                    //MessageBox.Show("Uncensor code needs updating");
                }
                */
            }
            
            
        }

        string gamePath;
        string assemblyPath;
        string enginePath;
        string dataPath;
        string strippedDataPath;
        string nocrcPath;
        string crcPath;
        FileStream fsa;
        FileStream fsu;
        byte[] ascs;
        byte[] pattern = new byte[] { 0x00, 0x06, 0x2a, 0x00, 0x13, 0x30, 0x09, 0x00, 0x9a }; 
        byte[] patternUncensored = new byte[] { 0x00, 0x06, 0x2a, 0x00, 0x0a, 0x16, 0x2a, 0x00, 0x9a };
        byte[] decensor = new byte[] { 0x0a, 0x16, 0x2a };
        byte[] censor = new byte[] { 0x13, 0x30, 0x09 };
        int pos;
        string md5Patched = "98652d81ab262cd76f8e1caf8cc0687e";
        string md5Takeownership = "d8394cdb0163849564e169edd5ac33f0";
        string currentBundleCRC = "";
        string targetCRC = "";
        string currentBundlePath;
        string currentTargetPath;
        Settings settings = new Settings();
        string iniPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.ini");
        JavaScriptSerializer js = new JavaScriptSerializer();
        


        private void button_patch_Click0(object sender, EventArgs e)
        {
            try
            {
                fsa = new FileStream(assemblyPath, FileMode.Open, FileAccess.Write, FileShare.Read);
                fsa.Close();
            }
            catch
            {
                MessageBox.Show("Remove write protection from Assembly-CSharp.dll first");
                return;
            }
            Task.Factory.StartNew(() => {
                disableControls();
                pos = GetPosition(ascs, pattern, 0);
                if (pos > 0)
                {
                    ascs = File.ReadAllBytes(assemblyPath);
                    for (int i = 0; i < pattern.Length; i++)
                    {
                        ascs[pos + i] = patternUncensored[i];
                    }
                    File.WriteAllBytes(assemblyPath, ascs);
                    button_patch.Invoke(new Action(() => button_patch.Text = "Uncensored"));
                    button_restore.Invoke(new Action(() => button_restore.Text = "Censor"));
                    button_patch.Invoke(new Action(() => button_patch.ForeColor = System.Drawing.Color.Green));
                    button_restore.Invoke(new Action(() => button_restore.ForeColor = System.Drawing.Color.Black));
                }
                enableControls();
            });
            
        }

        private void button_patch_Click(object sender, EventArgs e)
        {
            try
            {
                fsa = new FileStream(assemblyPath, FileMode.Open, FileAccess.Write, FileShare.Read);
                fsa.Close();
            }
            catch
            {
                MessageBox.Show("Remove write protection from Assembly-CSharp.dll first");
                return;
            }
            Task.Factory.StartNew(() => {
                disableControls();

                using (var client = new WebClient())
                {
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    client.Headers.Add("user-agent", "AGA");
                    client.DownloadFile("https://github.com/phantasmx/AGA-mods/raw/master/Uncensor/Assembly-CSharp.dll", assemblyPath + ".unc");
                }
                if (!File.Exists(assemblyPath + ".unc"))
                {
                    MessageBox.Show("Couldn't download uncensor file");
                }
                else
                {
                    File.Move(assemblyPath, assemblyPath + ".old");
                    File.Move(assemblyPath + ".unc", assemblyPath);
                    File.Delete(assemblyPath + ".old");
                    button_patch.Invoke(new Action(() => button_patch.Text = "Uncensored"));
                    button_restore.Invoke(new Action(() => button_restore.Text = "Censor"));
                    button_patch.Invoke(new Action(() => button_patch.ForeColor = System.Drawing.Color.Green));
                    button_restore.Invoke(new Action(() => button_restore.ForeColor = System.Drawing.Color.Black));
                }

                enableControls();
            });

        }


        private void button_restore_Click(object sender, EventArgs e)
        {
            try
            {
                fsa = new FileStream(assemblyPath, FileMode.Open, FileAccess.Write, FileShare.Read);
                fsa.Close();
            }
            catch
            {
                MessageBox.Show("Remove write protection from Assembly-CSharp.dll first");
                return;
            }
            Task.Factory.StartNew(() => {
                disableControls();
                pos = GetPosition(ascs, patternUncensored, 0);
                if (pos > 0)
                {
                    ascs = File.ReadAllBytes(assemblyPath);
                    for (int i = 0; i < patternUncensored.Length; i++)
                    {
                        ascs[pos + i] = pattern[i];
                    }
                    File.WriteAllBytes(assemblyPath, ascs);
                    button_patch.Invoke(new Action(() => button_patch.Text = "Uncensor"));
                    button_restore.Invoke(new Action(() => button_restore.Text = "Censored"));
                    button_patch.Invoke(new Action(() => button_patch.ForeColor = System.Drawing.Color.Black));
                    button_restore.Invoke(new Action(() => button_restore.ForeColor = System.Drawing.Color.Red));
                }
                enableControls();
            });
            
        }

        int GetPosition(byte[] data, byte[] pattern, int startIndex)
        {
            for (int i = startIndex; i < data.Length - pattern.Length; i++)
            {
                bool match = true;
                for (int k = 0; k < pattern.Length; k++)
                {
                    if (data[i + k] != pattern[k])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                {
                    return i;
                }
            
            }
            return -1;
        }

        int GetPositionReverse(byte[] data, byte[] pattern, int startIndex)
        {
            for (int i = startIndex; i > 0; i--)
            {
                bool match = true;
                for (int k = 0; k < pattern.Length; k++)
                {
                    if (data[i + k] != pattern[k])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                {
                    return i;
                }

            }
            return -1;
        }

        public void replaceBytes(byte[] data, byte[] pattern, int pos)
        {
            for (int i = 0; i < pattern.Length; i++)
            {
                data[pos + i] = pattern[i];
            }
        }

        private void button_launch_Click(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName("alice_main").Length == 0)
            {
                
                Task.Factory.StartNew(() => {
                    disableControls();
                    if (checkBox_lock_a.Checked) { fsa = new FileStream(assemblyPath, FileMode.Open, FileAccess.Read, FileShare.Read); }
                    if (checkBox_lock_u.Checked) { fsu = new FileStream(enginePath, FileMode.Open, FileAccess.Read, FileShare.Read); }

                    Process.Start("dmmgameplayer://alicegearaegisexe/cl/general/");

                    while (Process.GetProcessesByName("alice_main").Length == 0)
                    {
                        Thread.Sleep(1000);
                    }

                    if (checkBox_lock_a.Checked) { fsa.Close(); }
                    if (checkBox_lock_u.Checked) { fsu.Close(); }
                    enableControls();
                });
                
            }
                
        }


        public void disableControls()
        {
            button_launch.Invoke(new Action(() => button_launch.Enabled = false));
            checkBox_lock_a.Invoke(new Action(() => checkBox_lock_a.Enabled = false));
            checkBox_lock_u.Invoke(new Action(() => checkBox_lock_u.Enabled = false));
            /*button_patch.Invoke(new Action(() => button_patch.Enabled = false));
            button_restore.Invoke(new Action(() => button_restore.Enabled = false));
            button_patchTranslation.Invoke(new Action(() => button_patchTranslation.Enabled = false));
            button_updateTranslation.Invoke(new Action(() => button_updateTranslation.Enabled = false));
            button_permaLock.Invoke(new Action(() => button_permaLock.Enabled = false));*/
            tabControl1.Invoke(new Action(() => tabControl1.Enabled = false));
            
        }

        public void enableControls()
        {
            button_launch.Invoke(new Action(() => button_launch.Enabled = true));
            checkBox_lock_a.Invoke(new Action(() => checkBox_lock_a.Enabled = true));
            checkBox_lock_u.Invoke(new Action(() => checkBox_lock_u.Enabled = true));
            /*button_patch.Invoke(new Action(() => button_patch.Enabled = true));
            button_restore.Invoke(new Action(() => button_restore.Enabled = true));
            button_patchTranslation.Invoke(new Action(() => button_patchTranslation.Enabled = true));
            button_updateTranslation.Invoke(new Action(() => button_updateTranslation.Enabled = true));
            button_permaLock.Invoke(new Action(() => button_permaLock.Enabled = true));*/
            tabControl1.Invoke(new Action(() => tabControl1.Enabled = true));
        }

        private void button_updateTranslation_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => {
                disableControls();
                string md5 = CalculateMD5(enginePath);
                if (md5 != md5Patched)
                {
                    MessageBox.Show("Apply translation patch first");
                }
                else
                {
                    using (var client = new WebClient())
                    {
                        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        client.Headers.Add("user-agent", "AGA");
                        client.DownloadFile("https://github.com/Aceship/Alice-Gear-Aegis-EN-Text/archive/master.zip", gamePath + "\\translation.zip");
                    }
                    if (!File.Exists(gamePath + "\\translation.zip"))
                    {
                        MessageBox.Show("Couldn't download translation file");
                    }
                    else
                    {
                        if (Directory.Exists(gamePath + "\\Alice-Gear-Aegis-EN-Text-master"))
                        {
                            Directory.Delete(gamePath + "\\Alice-Gear-Aegis-EN-Text-master", true);
                        }
                        ZipFile.ExtractToDirectory(gamePath + "\\translation.zip", gamePath);
                        if (!File.Exists(gamePath + "\\Alice-Gear-Aegis-EN-Text-master\\AutoTranslator\\Translation\\en\\Text\\_AutoGeneratedTranslations.txt") &&
                            File.Exists(gamePath + "\\AutoTranslator\\Translation\\en\\Text\\_AutoGeneratedTranslations.txt"))
                        {
                            File.Copy(gamePath + "\\AutoTranslator\\Translation\\en\\Text\\_AutoGeneratedTranslations.txt", gamePath + "\\Alice-Gear-Aegis-EN-Text-master\\AutoTranslator\\Translation\\en\\Text\\_AutoGeneratedTranslations.txt");
                        }
                        if (Directory.Exists(gamePath + "\\AutoTranslator") && Directory.Exists(gamePath + "\\Alice-Gear-Aegis-EN-Text-master\\AutoTranslator"))
                        {

                            var diares = new Form2().ShowDialog();
                            if (diares == DialogResult.OK)
                            {
                                Directory.Delete(gamePath + "\\AutoTranslator", true);
                                Directory.Move(gamePath + "\\Alice-Gear-Aegis-EN-Text-master\\AutoTranslator", gamePath + "\\AutoTranslator");
                                Directory.Delete(gamePath + "\\Alice-Gear-Aegis-EN-Text-master", true);
                                File.Delete(gamePath + "\\translation.zip");
                                MessageBox.Show("Translation files were replaced");
                            }
                            else if (diares == DialogResult.Yes)
                            {
                                FileSystem.MoveDirectory(gamePath + "\\Alice-Gear-Aegis-EN-Text-master\\AutoTranslator", gamePath + "\\AutoTranslator", true);
                                Directory.Delete(gamePath + "\\Alice-Gear-Aegis-EN-Text-master", true);
                                File.Delete(gamePath + "\\translation.zip");
                                MessageBox.Show("Translation files were merged");
                            }
                            else if (diares == DialogResult.No)
                            {
                                if (Directory.Exists(gamePath + "\\AutoTranslator_Old"))
                                {
                                    Directory.Delete(gamePath + "\\AutoTranslator_Old", true);
                                }
                                Directory.Move(gamePath + "\\AutoTranslator", gamePath + "\\AutoTranslator_Old");
                                Directory.Move(gamePath + "\\Alice-Gear-Aegis-EN-Text-master\\AutoTranslator", gamePath + "\\AutoTranslator");
                                Directory.Delete(gamePath + "\\Alice-Gear-Aegis-EN-Text-master", true);
                                File.Delete(gamePath + "\\translation.zip");
                                MessageBox.Show("Translation files were updated\nOld files were moved to 'AutoTranslator_Old' folder");
                            }
                            else
                            {
                                MessageBox.Show("No changes were made");
                            }
                        }
                    }
                }
                enableControls();
            });

        }

        private void button_patchTranslation_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => {
                string md5 = CalculateMD5(enginePath);
                if (md5 == md5Patched)
                {
                    MessageBox.Show("Already Patched");
                }
                else
                {
                    try
                    {
                        fsu = new FileStream(enginePath, FileMode.Open, FileAccess.Write, FileShare.Read);
                        fsu.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Remove write protection from UnityEngine.CoreModule.dll first");
                        return;
                    }
                    disableControls();
                    using (var client = new WebClient())
                    {
                        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        client.Headers.Add("user-agent", "AGA");
                        client.DownloadFile("https://github.com/phantasmx/AGA-mods/raw/master/translation_patch.zip", gamePath + "\\translation_patch.zip");
                    }
                    if (!File.Exists(gamePath + "\\translation_patch.zip"))
                    {
                        MessageBox.Show("Couldn't download translation patch file");
                    }
                    else
                    {
                        ZipFile.ExtractToDirectory(gamePath + "\\translation_patch.zip", gamePath + "\\translation_patch");

                        if (Directory.Exists(gamePath + "\\translation_patch"))
                        {
                            //Directory.Move(gamePath + "\\translation_patch", gamePath);
                            FileSystem.MoveDirectory(gamePath + "\\translation_patch", gamePath, true);
                            //Directory.Delete(gamePath + "\\translation_patch", true);
                            File.Delete(gamePath + "\\translation_patch.zip");
                        }

                        md5 = CalculateMD5(enginePath);
                        if (md5 == md5Patched)
                        {
                            button_patchTranslation.Invoke(new Action(() => button_patchTranslation.Text = "Patched"));
                            button_patchTranslation.Invoke(new Action(() => button_patchTranslation.ForeColor = System.Drawing.Color.Green));
                            button_updateTranslation.Invoke(new Action(() => button_updateTranslation.Enabled = true));
                            MessageBox.Show("OK");
                        }
                        else
                        {
                            button_patchTranslation.Invoke(new Action(() => button_patchTranslation.Text = "Patch"));
                            button_patchTranslation.Invoke(new Action(() => button_patchTranslation.ForeColor = System.Drawing.Color.Black));
                            button_updateTranslation.Invoke(new Action(() => button_updateTranslation.Enabled = false));
                            MessageBox.Show("Patch unsuccessful");
                        }
                    }
                    enableControls();
                }
            });
            
        }


        static string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        private void button_permaLock_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => {
                disableControls();
                using (var client = new WebClient())
                {
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    client.Headers.Add("user-agent", "AGA");
                    client.DownloadFile("https://raw.githubusercontent.com/phantasmx/AGA-mods/master/takeownership.bat", gamePath + "\\alice_main_Data\\Managed\\takeownership.bat");
                }
                if (!File.Exists(gamePath + "\\alice_main_Data\\Managed\\takeownership.bat"))
                {
                    MessageBox.Show("Couldn't download takeownership.bat");
                }
                else
                {
                    string md5 = CalculateMD5(gamePath + "\\alice_main_Data\\Managed\\takeownership.bat");
                    if(md5 != md5Takeownership)
                    {
                        MessageBox.Show("Checksum of takeownership.bat does not match\n\nCheck the contents of takeownership.bat\nin \\alice_main_Data\\Managed\\ folder\nand run it manually as admin if you trust it");
                    }
                    else
                    {
                        string bat = gamePath + "\\alice_main_Data\\Managed\\takeownership.bat";
                        var proc = new ProcessStartInfo();
                        proc.CreateNoWindow = true;
                        proc.FileName = @"cmd.exe";
                        proc.Verb = "runas";
                        proc.Arguments = "/C " + bat;
                        try
                        {
                            var process = new Process();
                            process.StartInfo = proc;
                            process.Start();
                            process.WaitForExit();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("This feature needs administrator privileges to run");
                        }
                    }

                }
                enableControls();
            });
            
        }


        public static IEnumerable<string> EnumerateFilesSafely(string path, string searchPattern, System.IO.SearchOption searchOptions = System.IO.SearchOption.TopDirectoryOnly, Action<string> onAccessDenied = null)
        {
            try
            {
                var fileNames = Enumerable.Empty<string>();

                if (searchOptions == System.IO.SearchOption.AllDirectories)
                {
                    fileNames = Directory.EnumerateDirectories(path).SelectMany(x => EnumerateFilesSafely(x, searchPattern, searchOptions, onAccessDenied));
                }
                return fileNames.Concat(Directory.EnumerateFiles(path, searchPattern));
            }
            catch (UnauthorizedAccessException)
            {
                if (onAccessDenied != null)
                {
                    onAccessDenied(path);
                }
                return Enumerable.Empty<string>();
            }
        }

        private void button_stripCRC_Click(object sender, EventArgs e)
        {
            if (strippedDataPath == "" || dataPath == "")
            {
                MessageBox.Show("Set folders for original game data and stripped data first");
                return;
            }
            string suffix = textBox_suffix.Text.Trim();
            if (suffix.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                MessageBox.Show("Suffix textbox contains invalid characters");
                return;
            }
            
            Task.Factory.StartNew(() => {
                disableControls();
                string nocrcFilePath;
                string crcFilePath;
                string name;
                byte[] content;
                byte[] crc;
                byte[] nocrc;
                byte[] crc0;
                int count = 0;
                bool update;
                bool updatecrc;
                //add free space check
                if (!Directory.Exists(nocrcPath))
                {
                    Directory.CreateDirectory(nocrcPath);
                }
                if (!Directory.Exists(crcPath))
                {
                    Directory.CreateDirectory(crcPath);
                }
                foreach (var bundlePath in EnumerateFilesSafely(dataPath, "*.unity3d", System.IO.SearchOption.AllDirectories, null))
                {
                    name = Path.GetFileNameWithoutExtension(bundlePath);
                    nocrcFilePath = Path.Combine(nocrcPath, name + suffix + ".unity3d");
                    crcFilePath = Path.Combine(crcPath, name + ".crc");
                    //nocrcFilePath = bundlePath.Replace(dataPath, nocrcPath);
                    //crcFilePath = bundlePath.Replace(dataPath, crcPath).Replace(".unity3d", ".crc");

                    content = File.ReadAllBytes(bundlePath);
                    crc = new byte[32];
                    crc0 = new byte[32];
                    Array.Copy(content, 0, crc, 0, 32);
                    update = false;
                    if (File.Exists(crcFilePath))
                    {
                        crc0 = File.ReadAllBytes(crcFilePath);
                        if (!crc.SequenceEqual(crc0))
                        if (!crc.SequenceEqual(crc0))
                        if (!crc.SequenceEqual(crc0))
                        {
                            update = true;
                        }
                    }
                    updatecrc = !File.Exists(crcFilePath) || update;

                    if (!File.Exists(nocrcFilePath) || update)
                    {
                        //content = File.ReadAllBytes(bundlePath);
                        //crc = new byte[32];
                        nocrc = new byte[content.Length - 32];
                        //Array.Copy(content, 0, crc, 0, 32);
                        Array.Copy(content, 32, nocrc, 0, content.Length - 32);
                        File.WriteAllBytes(nocrcFilePath, nocrc);
                        //File.WriteAllBytes(crcFilePath, crc);
                        count++;
                    }
                    if (updatecrc)
                    {
                        File.WriteAllBytes(crcFilePath, crc);
                    }
                }
                enableControls();
                if(count == 0)
                {
                    MessageBox.Show("No new files found");
                }
                else
                {
                    MessageBox.Show(count.ToString() + " files were processed");
                }
                
            });
        }

        private void label_restoreCRC_DragDrop(object sender, DragEventArgs e)
        {
            Task.Factory.StartNew(() => {
                disableControls();
                string crcFilePath;
                byte[] content;
                byte[] crc;
                byte[] nocrc;
                foreach (string bundlePath in (string[])e.Data.GetData(DataFormats.FileDrop))
                {
                    nocrc = File.ReadAllBytes(bundlePath);
                    string name = Path.GetFileNameWithoutExtension(bundlePath).Substring(0, 32);
                    crcFilePath = Path.Combine(crcPath, name + ".crc");
                    if (!File.Exists(crcFilePath))
                    {
                        crcFilePath = Path.Combine(dataPath, name + ".unity3d");
                    }
                    if (File.Exists(crcFilePath) && nocrc[0] == 0x55 && nocrc[1] == 0x6e && nocrc[2] == 0x69 && nocrc[3] == 0x74 && nocrc[4] == 0x79)
                    {                        
                        crc = File.ReadAllBytes(crcFilePath);
                        content = new byte[nocrc.Length + 32];
                        Array.Copy(crc, 0, content, 0, 32);
                        Array.Copy(nocrc, 0, content, 32, content.Length - 32);
                        File.WriteAllBytes(bundlePath, content);
                    }
                }
                enableControls();
            });
        }

        private void label_restoreCRC_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }





        private void groupBox_source_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void groupBox_source_DragDrop(object sender, DragEventArgs e)
        {
            string[] drops = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (drops.Length > 1)
            {
                MessageBox.Show("Only one file allowed for swap");
            }
            else
            {
                Task.Factory.StartNew(() =>
                {
                    disableControls();
                    string bundlePath = drops[0];
                    string name = Path.GetFileNameWithoutExtension(bundlePath);
                    name = name.Substring(0, 32);
                    byte[] data;
                    FileStream fileStream = File.Open(bundlePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    BinaryReader reader = new BinaryReader(fileStream);
                    fileStream.Position = 0;
                    string h = new string(reader.ReadChars(5));
                    if (h == "CRC32")
                    {
                        fileStream.Position = 0;
                        //fileStream.Read(currentBundleCRC, 0, 32);
                        currentBundleCRC = new string(reader.ReadChars(32));
                        FileStream nocrcStream = File.Open(bundlePath + ".nocrc", FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
                        fileStream.CopyTo(nocrcStream);
                        nocrcStream.Position = 0;
                        data = bundleDecompressToMemory(nocrcStream);
                        nocrcStream.Close();
                        if (File.Exists(bundlePath + ".nocrc"))
                        {
                            File.Delete(bundlePath + ".nocrc");
                        }
                    }
                    else
                    {
                        currentBundleCRC = "";
                        data = bundleDecompressToMemory(fileStream);
                    }
                    fileStream.Close();


                    int pos = GetPosition(data, Encoding.ASCII.GetBytes("CAB-"), 0);//find cab name
                    byte[] cab = new byte[36];
                    for (int i = 0; i < 36; i++)
                    {
                        cab[i] = data[pos + i];
                    }
                    string cabName = Encoding.ASCII.GetString(cab);

                    byte[] pattern = Encoding.ASCII.GetBytes(".prefab");//find model code
                    pos = GetPosition(data, pattern, 0);
                    int pos2 = GetPositionReverse(data, new byte[] { 0x2f }, pos);
                    int pos3 = GetPosition(data, new byte[] { 0x5f }, pos2);
                    byte[] codeB = new byte[pos3 - pos2 + 6];
                    for (int i = 0; i < pos3 - pos2 + 6; i++)
                    {
                        codeB[i] = data[pos2 + 1 + i];
                    }
                    string code = Encoding.ASCII.GetString(codeB);
                    label_hint1.Invoke(new Action(() => label_hint1.Visible = false));
                    label_name.Invoke(new Action(() => label_name.Text = name));
                    label_code.Invoke(new Action(() => label_code.Text = code));
                    label_cab.Invoke(new Action(() => label_cab.Text = cabName));
                    label_crc.Invoke(new Action(() => label_crc.Text = currentBundleCRC));
                    currentBundlePath = bundlePath;
                    enableControls();
                });
            }
        }

        private void groupBox_target_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void groupBox_target_DragDrop(object sender, DragEventArgs e)
        {
            string[] drops = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (drops.Length > 1)
            {
                MessageBox.Show("Only one file allowed for swap");
            }
            else
            {
                Task.Factory.StartNew(() =>
                {
                    disableControls();
                    string bundlePath = drops[0];
                    string name = Path.GetFileNameWithoutExtension(bundlePath);
                    name = name.Substring(0, 32);
                    byte[] data;
                    FileStream fileStream = File.Open(bundlePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    BinaryReader reader = new BinaryReader(fileStream);
                    fileStream.Position = 0;
                    string h = new string(reader.ReadChars(5));
                    if (h == "CRC32")
                    {
                        fileStream.Position = 0;
                        //fileStream.Read(targetCRC, 0, 32);
                        targetCRC = new string(reader.ReadChars(32));
                        FileStream nocrcStream = File.Open(bundlePath + ".nocrc", FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
                        fileStream.CopyTo(nocrcStream);
                        nocrcStream.Position = 0;
                        data = bundleDecompressToMemory(nocrcStream);
                        nocrcStream.Close();
                        if (File.Exists(bundlePath + ".nocrc"))
                        {
                            File.Delete(bundlePath + ".nocrc");
                        }
                    }
                    else
                    {
                        targetCRC = "";
                        data = bundleDecompressToMemory(fileStream);
                    }
                    fileStream.Close();


                    int pos = GetPosition(data, Encoding.ASCII.GetBytes("CAB-"), 0);//find cab name
                    byte[] cab = new byte[36];
                    for (int i = 0; i < 36; i++)
                    {
                        cab[i] = data[pos + i];
                    }
                    string cabName = Encoding.ASCII.GetString(cab);




                    byte[] pattern = Encoding.ASCII.GetBytes(".prefab");
                    pos = GetPosition(data, pattern, 0);
                    int pos2 = GetPositionReverse(data, new byte[] { 0x2f }, pos);
                    int pos3 = GetPosition(data, new byte[] { 0x5f }, pos2);
                    byte[] codeB = new byte[pos3 - pos2 + 6];
                    for (int i = 0; i < pos3 - pos2 + 6; i++)
                    {
                        codeB[i] = data[pos2 + 1 + i];
                    }
                    string code = Encoding.ASCII.GetString(codeB);
                    textBox_name.Invoke(new Action(() => textBox_name.Text = name));
                    textBox_code.Invoke(new Action(() => textBox_code.Text = code));
                    textBox_cab.Invoke(new Action(() => textBox_cab.Text = cabName));
                    textBox_crc.Invoke(new Action(() => textBox_crc.Text = targetCRC));
                    currentTargetPath = bundlePath;
                    enableControls();
                });
            }
        }

        private void button_swap_Click(object sender, EventArgs e)
        {
            string name = label_name.Text;
            string code = label_code.Text;
            string cab = label_cab.Text;
            string nameNew = textBox_name.Text;
            string codeNew = textBox_code.Text;
            string cabNew = textBox_cab.Text;
            targetCRC = textBox_crc.Text;
            string bundlePath = currentBundlePath;
            

            if (!File.Exists(bundlePath) || name.Length != 32 || name.Length != nameNew.Length || code.Length != codeNew.Length || cab.Length != 36 || cab.Length != cabNew.Length)
            {
                MessageBox.Show("Name length or code length do not match");
            }
            else
            {
                //byte[] data = File.ReadAllBytes(bundlePath);
                
                byte[] data;
                FileStream fileStream = File.Open(bundlePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                BinaryReader reader = new BinaryReader(fileStream);
                fileStream.Position = 0;
                string h = new string(reader.ReadChars(5));
                if (h == "CRC32")
                {
                    fileStream.Position = 32;
                    //fileStream.Read(currentBundleCRC, 0, 32);
                    FileStream nocrcStream = File.Open(bundlePath + ".nocrc", FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
                    fileStream.CopyTo(nocrcStream);
                    nocrcStream.Position = 0;
                    data = bundleDecompressToMemory(nocrcStream);
                    nocrcStream.Close();
                    if (File.Exists(bundlePath + ".nocrc"))
                    {
                        File.Delete(bundlePath + ".nocrc");
                    }
                }
                else
                {
                    data = bundleDecompressToMemory(fileStream);
                }
                fileStream.Close();

                byte[] pattern = Encoding.ASCII.GetBytes("hd/avatar/parts/" + name + ".unity3d");
                int pos = GetPosition(data, pattern, 0);
                int pos2 = GetPosition(data, pattern, pos + 1);
                pattern = Encoding.ASCII.GetBytes("hd/avatar/parts/" + nameNew + ".unity3d");
                if (pos != -1)
                {
                    replaceBytes(data, pattern, pos);
                }
                if (pos2 != -1)
                {
                    replaceBytes(data, pattern, pos2);
                }

                pattern = Encoding.ASCII.GetBytes(code);
                int pos3 = GetPosition(data, pattern, pos);
                while (pos3 < pos2 && pos3 != -1)//replace all codes in assetbundle
                {
                    replaceBytes(data, Encoding.ASCII.GetBytes(codeNew), pos3);
                    pos3 = GetPosition(data, pattern, pos);
                }

                pos = GetPosition(data, Encoding.ASCII.GetBytes("_fbxAvatar"), 0);//replace code in avatar
                if (pos != -1)
                {
                    pos = pos - code.Length - 4;
                    replaceBytes(data, Encoding.ASCII.GetBytes(codeNew), pos);
                }

                pattern = new byte[4 + code.Length];//replace all codes in base objects
                pattern[0] = 0x0f;
                Array.Copy(Encoding.ASCII.GetBytes(code), 0, pattern, 4, code.Length);
                pos = 0;
                pos3 = GetPosition(data, pattern, pos);
                while (pos3 < data.Length - pattern.Length && pos3 != -1)
                {
                    replaceBytes(data, Encoding.ASCII.GetBytes(codeNew), pos3 + 4);
                    pos3 = GetPosition(data, pattern, pos);
                    pos = pos3 + code.Length;
                }
                pattern[0] = 0x0b;
                pos = 0;
                pos3 = GetPosition(data, pattern, pos);
                while (pos3 < data.Length - pattern.Length && pos3 != -1)
                {
                    replaceBytes(data, Encoding.ASCII.GetBytes(codeNew), pos3 + 4);
                    pos3 = GetPosition(data, pattern, pos);
                    pos = pos3 + code.Length;
                }

                pos = GetPosition(data, Encoding.ASCII.GetBytes("CAB-"), 0);//replace cab name
                byte[] cabName = Encoding.ASCII.GetBytes(cabNew);
                for (int i = 0; i < 36; i++)
                {
                    data[pos + i] = cabName[i];
                }
                
                if (targetCRC.Length == 32)//restore crc
                {
                    byte[] data2 = new byte[data.Length + 32];
                    Array.Copy(Encoding.ASCII.GetBytes(targetCRC), 0, data2, 0, 32);
                    Array.Copy(data, 0, data2, 32, data.Length);
                    data = data2;
                }

                string path = Path.GetDirectoryName(bundlePath);
                string ext = Path.GetExtension(bundlePath);
                string pathNew = Path.Combine(path, nameNew + ext);

                if (pathNew == currentTargetPath)
                {
                    if(File.Exists(currentTargetPath + ".bak"))
                    {
                        File.Delete(currentTargetPath + ".bak");
                    }
                    File.Move(currentTargetPath, currentTargetPath + ".bak");
                    
                }

                File.WriteAllBytes(pathNew, data);
                MessageBox.Show("Completed");
            }
        }

        private void label_stripCRC_DragDrop(object sender, DragEventArgs e)
        {
            Task.Factory.StartNew(() => {
                disableControls();
                string crcFilePath;
                byte[] crc;
                byte[] data;
                FileStream nocrcStream;
                FileStream fileStream;
                BinaryReader reader;
                if (!Directory.Exists(crcPath))
                {
                    Directory.CreateDirectory(crcPath);
                }
                foreach (string bundlePath in (string[])e.Data.GetData(DataFormats.FileDrop))
                {
                    string name = Path.GetFileNameWithoutExtension(bundlePath);
                    name = name.Substring(0, 32);
                    fileStream = File.Open(bundlePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    reader = new BinaryReader(fileStream);
                    fileStream.Position = 0;
                    string h = new string(reader.ReadChars(5));
                    fileStream.Position = 0;
                    if (h == "CRC32")
                    {
                        crcFilePath = Path.Combine(crcPath, name + ".crc");                        
                        crc = reader.ReadBytes(32);
                        if (!File.Exists(crcFilePath))
                        {
                            File.WriteAllBytes(crcFilePath, crc);
                        }

                        nocrcStream = File.Open(bundlePath + ".nocrc", FileMode.Create, FileAccess.ReadWrite, FileShare.Read);                        
                        fileStream.CopyTo(nocrcStream);
                        fileStream.Close();
                        nocrcStream.Position = 0;
                        data = bundleDecompressToMemory(nocrcStream);
                        nocrcStream.Close();
                        if (File.Exists(bundlePath + ".nocrc"))
                        {
                            File.Delete(bundlePath + ".nocrc");
                        }
                        File.WriteAllBytes(bundlePath, data);
                    }
                    else if(h == "Unity")
                    {
                        data = bundleDecompressToMemory(fileStream);
                        fileStream.Close();
                        File.WriteAllBytes(bundlePath, data);
                    }
                    

                }
                enableControls();
            });
        }

        private void label_stripCRC_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        /*
        private bool isCompressed(byte[] data)
        {
            int count = 0;
            int pos = 0;
            int pos2 = GetPosition(data, Encoding.ASCII.GetBytes(".unity3d"), pos);            
            while (pos2 != -1 && pos2 < data.Length - 8)
            {
                count++;
                pos = pos2 + 8;
                pos2 = GetPosition(data, Encoding.ASCII.GetBytes(".unity3d"), pos);
            }
            if (count < 2)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }*/

        /*
        private void uabeDecompress(string bundlePath)
        {
            Task.Factory.StartNew(() => {
                string listPath = bundlePath.Replace(".unity3d", ".txt");
                byte[] content = File.ReadAllBytes(bundlePath);
                int pos = GetPosition(content, Encoding.ASCII.GetBytes("CAB-"), 0);
                byte[] cabName = new byte[36];
                for (int i = 0; i < 36; i++)
                {
                    cabName[i] = content[pos + i];
                }
                string assetsPath = bundlePath.Replace(".unity3d", "_" + Encoding.ASCII.GetString(cabName) + ".assets");

                File.WriteAllText(listPath, "+FILE " + bundlePath);
                int t = 0;
                while (!File.Exists(listPath) || t > 2000)
                {
                    Thread.Sleep(100);
                    t += 100;
                }
                var process = Process.Start(uabePath, "-fd -kd batchexport \"" + listPath + "\"");
                process.WaitForExit();
                File.Delete(listPath);
                if (File.Exists(bundlePath + ".decomp"))
                {
                    File.Delete(bundlePath);
                    File.Move(bundlePath + ".decomp", bundlePath);
                    if (File.Exists(assetsPath))
                    {
                        File.Delete(assetsPath);
                    }
                }
            });
        }*/




        private byte[] bundleDecompressToMemory(FileStream filePath)
        {
            AssetsManager helper = new AssetsManager();
            BundleFileInstance inst = helper.LoadBundleFile(filePath);
            AssetBundleFile file = inst.file;
            file.reader.Position = 0;
            MemoryStream stream = new MemoryStream();
            file.Unpack(file.reader, new AssetsFileWriter(stream));
            byte[] bytes = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(bytes, 0, (int)stream.Length);
            stream.Close();
            return bytes;
            //File.WriteAllBytes(filePath + ".unpacked", bytes);

        }



        public class Settings
        {
            public string dataPath { get; set; }
            public string strippedDataPath { get; set; }
            public int activeTab { get; set; }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.activeTab = tabControl1.SelectedIndex;
            File.WriteAllText(iniPath, js.Serialize(settings).Replace(",", ",\n\t").Replace("{", "{\n\t").Replace("}", "\n}"));
        }

        private void button_strippeddataPath_Click(object sender, EventArgs e)
        {
            VistaFolderBrowserDialog folderBrowserDialog = new VistaFolderBrowserDialog();
            if(strippedDataPath != "")
            {
                folderBrowserDialog.SelectedPath = strippedDataPath + "\\";
            }
            else
            {
                folderBrowserDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
            }
            
            DialogResult dialogResult = folderBrowserDialog.ShowDialog();
            if(dialogResult == DialogResult.OK)
            {
                strippedDataPath = folderBrowserDialog.SelectedPath;
                if (strippedDataPath != "" && Directory.Exists(strippedDataPath))
                {
                    nocrcPath = strippedDataPath;
                    crcPath = strippedDataPath + @"\_crc\";
                    label_strippedDataPath.Text = strippedDataPath;
                    settings.strippedDataPath = strippedDataPath.Replace(AppDomain.CurrentDomain.BaseDirectory, "");
                    File.WriteAllText(iniPath, js.Serialize(settings).Replace(",", ",\n\t").Replace("{", "{\n\t").Replace("}", "\n}"));
                }
            }            
        }

        private void button_gamedataPC_Click(object sender, EventArgs e)
        {
            VistaFolderBrowserDialog folderBrowserDialog = new VistaFolderBrowserDialog();
            if (dataPath != "")
            {
                folderBrowserDialog.SelectedPath = dataPath + "\\";
            }
            else
            {
                folderBrowserDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
            }
            
            DialogResult dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                dataPath = folderBrowserDialog.SelectedPath;
                if (dataPath != "" && Directory.Exists(dataPath))
                {
                    label_dataPath.Text = dataPath;
                    settings.dataPath = dataPath;
                    File.WriteAllText(iniPath, js.Serialize(settings).Replace(",", ",\n\t").Replace("{", "{\n\t").Replace("}", "\n}"));
                }
            }
        }

        private void button_help_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    MessageBox.Show(@"AGA Helper

    Includes:
    Uncensor
    Translation patch
    Patched files locking
    Launcher
    Modding tools

    Game path is detected by registry.If for some reason this doesn't work, try to run utility from game folder or with admin privileges.

    Use `Uncensor` button to remove Access Denied censorship - this will modify Assembly - CSharp.dll.

    Use `Patch` button to download and apply translation patch - this will modify UnityEngine.CoreModule.dll.

    Use `Get Latest Translation` button to download and apply latest translation files.

    If running the game through the `Launch` button, use checkboxes to protect patched Assembly - CSharp.dll(uncensor) and UnityEngine.CoreModule.dll(translation) from updating by DMM client.
    During game updates, uncheck `Lock Assembly - CSharp.dll` to allow its updating, and reapply uncensor afterwards.

    Or use `Permanent Lock / Unlock` button to lock files by setting file permissions for UnityEngine.CoreModule and Assembly-CSharp, and run game as usual through DMM client.
    This needs administrator privileges, and may not work on non - English Windows versions.

    Translation patch is made using https://github.com/bbepis/XUnity.AutoTranslator 

    Translation is downloaded from https://github.com/Aceship/Alice-Gear-Aegis-EN-Text

    Permanent Lock / Unlock batch is downloaded from https://github.com/Globalnet626/random/blob/master/takeownership.bat");
                    break;

                case 1:
                    MessageBox.Show(@"Model Swap usage:

- Drag&Drop '.unity3d' file with the source (replacing) model to the 'Current' panel.

- Drag&Drop '.unity3d' file with the target (replaced) model to the 'Change to' panel or enter new values manually (CRC is optional).

- Click 'Swap' button.

Swapped file will be created in the same folder as the source file.
Does not require stripping CRC header or decompressing.
If CRC header is present in the target file, it will automatically be restored to the created file. 

Currently works with bodies, faces, hairs, accessories and gears.
Probably can work with other simple models (with single model code).");
                    break;
            }
        }

        private void button_mcodeSearch_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(dataPath))
            {
                MessageBox.Show("Game data folder is not set correctly");
                return;
            }
            byte[] data;
            string text = textBox_mcodeSearch.Text;
            if (text == "")
            {
                MessageBox.Show("Enter the model code in the textbox");
                return;
            }
            byte[] code = Encoding.ASCII.GetBytes(text);
            int pos = 0;
            StringCollection paths = new StringCollection();
            StringCollection names = new StringCollection();
            

            Task.Factory.StartNew(() => {
                disableControls();
                foreach (var bundlePath in EnumerateFilesSafely(dataPath, "*.unity3d", System.IO.SearchOption.AllDirectories, null))
                {
                    data = File.ReadAllBytes(bundlePath);
                    
                    pos = GetPosition(data, code, 0);
                    if(pos > 0)
                    {
                        paths.Add(bundlePath);
                        names.Add(Path.GetFileName(bundlePath));
                    }
                }
                enableControls();
                if (paths.Count > 0)
                {
                    Invoke((Action)(() => { Clipboard.SetFileDropList(paths); }));
                    MessageBox.Show("Following files containing \"" + text + "\" were found and copied to clipboard:\n\n" + string.Join("\n", names.Cast<string>().ToArray()));
                }
                else
                {
                    MessageBox.Show("Not found");
                }
            });

        }

        private void button_mcodeSearchStripped_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(strippedDataPath))
            {
                MessageBox.Show("Stripped data folder is not set correctly");
                return;
            }
            byte[] data;
            string text = textBox_mcodeSearch.Text;
            if (text == "")
            {
                MessageBox.Show("Enter the model code in the textbox");
                return;
            }
            byte[] code = Encoding.ASCII.GetBytes(text);
            int pos = 0;
            StringCollection paths = new StringCollection();
            StringCollection names = new StringCollection();


            Task.Factory.StartNew(() => {
                disableControls();
                foreach (var bundlePath in EnumerateFilesSafely(strippedDataPath, "*.unity3d", System.IO.SearchOption.AllDirectories, null))
                {
                    data = File.ReadAllBytes(bundlePath);

                    pos = GetPosition(data, code, 0);
                    if (pos > 0)
                    {
                        paths.Add(bundlePath);
                        names.Add(Path.GetFileName(bundlePath));
                    }
                }
                enableControls();
                if (paths.Count > 0)
                {
                    Invoke((Action)(() => { Clipboard.SetFileDropList(paths); }));
                    MessageBox.Show("Following files containing \"" + text + "\" were found and copied to clipboard:\n\n" + string.Join("\n", names.Cast<string>().ToArray()));
                }
                else
                {
                    MessageBox.Show("Not found");
                }
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*var devices = MediaDevice.GetDevices();
            var dev = string.Join("\n", devices.Cast<string>().ToArray());
            MessageBox.Show(dev);return;
            using (var device = devices.First(d => d.FriendlyName == "Galaxy S8+"))
            {
                device.Connect();

                var photoDir = device.GetDirectoryInfo(@"\Phone\Android\data\jp.colopl.alice\files");

                var files = photoDir.EnumerateFiles("*.unity3d", System.IO.SearchOption.TopDirectoryOnly);

                foreach (var file in files)
                {
                    MemoryStream memoryStream = new MemoryStream();
                    device.DownloadFile(file.FullName, memoryStream);
                    memoryStream.Position = 0;
                    //WriteSreamToDisk($@"D:\PHOTOS\{file.Name}", memoryStream);
                }
                device.Disconnect();


                using (FileStream stream = File.OpenWrite(@"D:/_DEL/36140c37a10c0b1a7a77000909745237.unity3d"))
                {
                    device.DownloadFile(@"\Phone\Android\data\jp.colopl.alice\files\36140c37a10c0b1a7a77000909745237.unity3d", stream);
                }
                device.Disconnect();
            }*/

        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            string meshName = "mesh_cos_body_hom";
            string path_level0 = @"d:\_DEL\aga\dump_level0\";
            string path_unity3d = @"d:\_DEL\aga\dump_unity3d\";
            string meshFilePath = @"d:\_DEL\aga\mesh_mod.txt";
            string monoPath = @"d:\_DEL\aga\mono_mod.txt";
            string file;
            string data;
            string[] meshDataNew;
            string[] meshData;
            string[] monoData;
            string[] buffer = new string[4];
            int pos = 0;
            int len = 0;
            int pos0;
            int count;
            string m_PathID = "";
            int bonesCount = 0;
            int bonesCountOriginal = 0;
            string[] boneNamesOriginal;
            string[] boneNamesMod;
            string[] boneNamesOriginalFiltered;
            string[] bonePathIDsMod;
            string m_RootBonePathID;
            int boneIndex;
            string boneIndexText;
            string data2;
            string dataBindArray;


            file = Directory.GetFiles(path_level0, meshName + "*GameObject.txt").FirstOrDefault();
            if(file == null)
            {
                MessageBox.Show("level0 GameObject not found");
                return;
            }
            data = File.ReadAllText(file);
            pos = data.IndexOf("[1]");
            pos = data.IndexOf("m_PathID", pos) + 11;
            len = data.IndexOf("\r\n", pos) - pos;
            m_PathID = data.Substring(pos, len);

            file = Directory.GetFiles(path_level0, "*-" + m_PathID + "-SkinnedMeshRenderer.txt").FirstOrDefault();
            if (file == null)
            {
                MessageBox.Show("level0 SMR not found");
                return;
            }

            data = File.ReadAllText(file);
            pos = data.IndexOf("vector m_Bones");
            pos = data.IndexOf("int size", pos) + 11;
            len = data.IndexOf("\r\n", pos) - pos;
            bonesCount = int.Parse(data.Substring(pos, len));
            bonePathIDsMod = new string[bonesCount];
            boneNamesMod = new string[bonesCount];
            for (int i = 0; i < bonesCount; i++)
            {
                pos = data.IndexOf("m_PathID", pos) + 11;
                len = data.IndexOf("\r\n", pos) - pos;
                bonePathIDsMod[i] = data.Substring(pos, len);
            }
            pos = data.IndexOf("m_RootBone", pos);
            pos = data.IndexOf("m_PathID", pos) + 11;
            len = data.IndexOf("\r\n", pos) - pos;
            m_RootBonePathID = data.Substring(pos, len);
            

            for (int i = 0; i < bonesCount; i++)
            {
                file = Directory.GetFiles(path_level0, "*-" + bonePathIDsMod[i] + "-Transform.txt").FirstOrDefault();
                if (file == null)
                {
                    MessageBox.Show("level0 " + bonePathIDsMod[i] + "transform not found");
                    return;
                }
                data = File.ReadAllText(file);
                pos = data.IndexOf("m_GameObject");
                pos = data.IndexOf("m_PathID", pos) + 11;
                len = data.IndexOf("\r\n", pos) - pos;
                m_PathID = data.Substring(pos, len);

                file = Directory.GetFiles(path_level0, "*-" + m_PathID + "-GameObject.txt").FirstOrDefault();
                if (file == null)
                {
                    MessageBox.Show("level0 " + m_PathID + "GameObject not found");
                    return;
                }
                data = File.ReadAllText(file);
                pos = data.IndexOf("string m_Name") +17;
                len = data.IndexOf("\"", pos) - pos;
                boneNamesMod[i] = data.Substring(pos, len);

                

            }

            file = Directory.GetFiles(path_unity3d, meshName + "*GameObject.txt").FirstOrDefault();
            if (file == null)
            {
                MessageBox.Show("original GameObject not found");
                return;
            }
            data = File.ReadAllText(file);
            pos = data.IndexOf("[2]");
            pos = data.IndexOf("m_PathID", pos) + 11;
            len = data.IndexOf("\r\n", pos) - pos;
            m_PathID = data.Substring(pos, len);
            if (m_PathID.StartsWith("-"))
            {
                m_PathID = m_PathID.Trim('-');
                ulong d_PathID = ulong.Parse(m_PathID);
                d_PathID = ulong.MaxValue - d_PathID + 1;
                m_PathID = d_PathID.ToString();
            }

            file = Directory.GetFiles(path_unity3d, "*-" + m_PathID + "-MonoBehaviour.txt").FirstOrDefault();
            if (file == null)
            {
                MessageBox.Show("original MonoBehaviour not found");
                return;
            }
            
            data = File.ReadAllText(file);
            pos = data.IndexOf("vector boneNames_");
            pos = data.IndexOf("int size", pos) + 11;
            len = data.IndexOf("\r\n", pos) - pos;
            bonesCountOriginal = int.Parse(data.Substring(pos, len));
            
            

            boneNamesOriginal = new string[bonesCountOriginal];
            for (int i = 0; i < bonesCountOriginal; i++)
            {
                pos = data.IndexOf("string data", pos) + 15;
                len = data.IndexOf("\"", pos) - pos;
                boneNamesOriginal[i] = data.Substring(pos, len);
            }

            if (bonesCountOriginal == bonesCount)
            {
                boneNamesOriginalFiltered = boneNamesOriginal;
            }
            else if (bonesCountOriginal > bonesCount)
            {
                boneNamesOriginalFiltered = new string[bonesCount];
                boneNamesOriginalFiltered = boneNamesOriginal.Where(val => boneNamesMod.Contains(val)).ToArray();
            }
            else
            {
                MessageBox.Show("Bone count does not match");
                return;
            }

            file = Directory.GetFiles(path_level0, meshName + "*Mesh.txt").FirstOrDefault();
            if (file == null)
            {
                MessageBox.Show("mod mesh not found");
                return;
            }
            meshData = File.ReadAllLines(file);
            count = meshData.Length;
            meshDataNew = new string[count];
            pos = 0;

            while (!meshData[pos].Contains("m_BoneNameHashes"))
            {
                meshDataNew[pos] = meshData[pos];
                pos++;
            }

            meshDataNew[pos] = meshData[pos];
            pos++;
            meshDataNew[pos] = meshData[pos];
            pos++;
            meshDataNew[pos] = meshData[pos];
            pos++;
            pos0 = pos;
            for(int i = 0; i < bonesCount; i++)
            {
                boneIndex = Array.IndexOf(boneNamesOriginalFiltered, boneNamesMod[i]);

                if (boneIndex == -1)
                {
                    MessageBox.Show("skeletons does not match");
                    return;
                }
                
                meshDataNew[pos] = meshData[pos];
                pos++;
                meshDataNew[pos] = meshData[pos0 + boneIndex * 2 + 1];
                pos++;
            }

            while(pos < count)
            {
                if (meshData[pos].Contains("boneIndex"))
                {
                    boneIndexText = meshData[pos].Substring(meshData[pos].IndexOf("boneIndex") + 15);
                    boneIndex = int.Parse(boneIndexText);
                    boneIndex = Array.IndexOf(boneNamesOriginalFiltered, boneNamesMod[boneIndex]);
                    meshDataNew[pos] = meshData[pos].Replace("= " + boneIndexText, "= " + boneIndex.ToString());
                    pos++;
                }
                else
                {
                    meshDataNew[pos] = meshData[pos];
                    pos++;
                }
            }
            
            File.WriteAllLines(meshFilePath, meshDataNew);

            data2 = File.ReadAllText(meshFilePath);

            pos = data.IndexOf("vector bindPoses_");
            pos = data.IndexOf("0 Array Array", pos);            
            dataBindArray = data.Substring(pos);

            string boneNames_Array;
            pos = data.IndexOf("vector boneNames_");
            pos = data.IndexOf("0 Array Array", pos);
            len = data.IndexOf(" 0 PPtr<$SkinnedMeshRenderer> skin_", pos) - pos;
            boneNames_Array = data.Substring(pos, len);

            List<string> bindPoses = new List<string>();
            List<string> boneNames_ = new List<string>();

            if (bonesCountOriginal > bonesCount)
            {
                pos = 0;
                count = 0;
                while (pos < dataBindArray.Length && count < bonesCountOriginal)
                {
                    pos = dataBindArray.IndexOf("]", pos);
                    len = dataBindArray.IndexOf("[", pos) - pos;
                    if (len < 0)
                    {
                        len = dataBindArray.Length - pos;
                    }
                    if (Array.IndexOf(boneNamesOriginalFiltered, boneNamesOriginal[count]) != -1)
                    {
                        bindPoses.Add(dataBindArray.Substring(pos, len));
                    }

                    count++;
                    pos++;
                }

                dataBindArray = dataBindArray.Substring(0, dataBindArray.IndexOf("[")).Replace(bonesCountOriginal.ToString(),bonesCount.ToString());
                for(int i = 0; i < bonesCount; i++)
                {
                    dataBindArray += "[" + i.ToString() + bindPoses[i];
                }
                dataBindArray = dataBindArray.TrimEnd(' ');

                pos = data.IndexOf("vector bindPoses_");
                pos = data.IndexOf("0 Array Array", pos);
                data = data.Substring(0, pos) + dataBindArray;


                pos = 0;
                count = 0;
                while (pos < boneNames_Array.Length && count < bonesCountOriginal)
                {
                    pos = boneNames_Array.IndexOf("]", pos);
                    len = boneNames_Array.IndexOf("[", pos) - pos;
                    if (len < 0)
                    {
                        len = boneNames_Array.Length - pos;
                    }
                    if (Array.IndexOf(boneNamesOriginalFiltered, boneNamesOriginal[count]) != -1)
                    {
                        boneNames_.Add(boneNames_Array.Substring(pos, len));
                    }

                    count++;
                    pos++;
                }

                boneNames_Array = boneNames_Array.Substring(0, boneNames_Array.IndexOf("[")).Replace(bonesCountOriginal.ToString(), bonesCount.ToString());
                for (int i = 0; i < bonesCount; i++)
                {
                    boneNames_Array += "[" + i.ToString() + boneNames_[i];
                }
                boneNames_Array = boneNames_Array.TrimEnd(' ');
                pos = data.IndexOf("vector boneNames_");
                pos = data.IndexOf("0 Array Array", pos);
                len = data.IndexOf(" 0 PPtr<$SkinnedMeshRenderer> skin_", pos) - pos;
                data = data.Substring(0, pos) + boneNames_Array + data.Substring(pos + len);
            }
            

            pos = data2.IndexOf("vector m_BindPose");
            pos = data2.IndexOf("0 Array Array", pos);
            len = data2.IndexOf(" 0 vector m_BoneNameHashes", pos) - pos;
            data2 = data2.Substring(0, pos) + dataBindArray + data2.Substring(pos + len);
            File.WriteAllText(meshFilePath, data2);

            pos = data2.IndexOf("vector m_Skin");
            pos = data2.IndexOf("0 Array Array", pos);
            len = data2.IndexOf(" 1 VertexData m_VertexData", pos) - pos;
            data2 = data2.Substring(pos, len);


            pos = data.IndexOf("originalBoneWeights_");
            pos = data.IndexOf("0 Array Array", pos);
            len = data.IndexOf(" 0 vector bindPoses_", pos) - pos;

            data = data.Substring(0, pos) + data2 + data.Substring(pos + len);
            File.WriteAllText(monoPath, data);

            monoData = File.ReadAllLines(monoPath);

            pos = 0;
            count = monoData.Length;
            while (pos < count)
            {
                if (monoData[pos].Contains("BoneInfluence"))
                {
                    buffer[0] = monoData[pos + 1];
                    buffer[1] = monoData[pos + 2];
                    buffer[2] = monoData[pos + 3];
                    buffer[3] = monoData[pos + 4];

                    monoData[pos] = monoData[pos].Replace("BoneInfluence", "BoneWeight");
                    monoData[pos + 1] = monoData[pos + 5].Replace("[", "").Replace("]", "");
                    monoData[pos + 2] = monoData[pos + 6].Replace("[", "").Replace("]", "");
                    monoData[pos + 3] = monoData[pos + 7].Replace("[", "").Replace("]", "");
                    monoData[pos + 4] = monoData[pos + 8].Replace("[", "").Replace("]", "");
                    monoData[pos + 5] = buffer[0].Replace("[", "").Replace("]", "");
                    monoData[pos + 6] = buffer[1].Replace("[", "").Replace("]", "");
                    monoData[pos + 7] = buffer[2].Replace("[", "").Replace("]", "");
                    monoData[pos + 8] = buffer[3].Replace("[", "").Replace("]", "");
                    pos += 9;
                }
                pos++;
            }
            File.WriteAllLines(monoPath, monoData);
            /*
            File.WriteAllLines(@"k:\Mega\Links\alice gear aegis modding\kaede\machi\bonesMod.txt", boneNamesMod);
            File.WriteAllLines(@"k:\Mega\Links\alice gear aegis modding\kaede\machi\bonesOriginal.txt", boneNamesOriginal);
            File.WriteAllLines(@"k:\Mega\Links\alice gear aegis modding\kaede\machi\bonesOriginalFiltered.txt", boneNamesOriginalFiltered);
            Array.Sort(boneNamesMod);
            Array.Sort(boneNamesOriginal);
            Array.Sort(boneNamesOriginalFiltered);
            File.WriteAllLines(@"k:\Mega\Links\alice gear aegis modding\kaede\machi\bonesModSorted.txt", boneNamesMod);
            File.WriteAllLines(@"k:\Mega\Links\alice gear aegis modding\kaede\machi\bonesOriginalSorted.txt", boneNamesOriginal);
            File.WriteAllLines(@"k:\Mega\Links\alice gear aegis modding\kaede\machi\bonesOriginalFilteredSorted.txt", boneNamesOriginalFiltered);
            */
            MessageBox.Show("Completed");
        }

        private void label_boneOrderFix_DragDrop(object sender, DragEventArgs e)
        {
            string meshName = "";
            string path_level0 = "";
            string path_unity3d = "";
            string path_mesh = "";
            string path_root = "";
            string meshFilePath = "";
            string monoPath = "";
            string file;
            string data;
            string[] meshDataNew;
            string[] meshData;
            string[] monoData;
            string[] buffer = new string[4];
            int pos = 0;
            int len = 0;
            int pos0;
            int count;
            string m_PathID = "";
            int bonesCount = 0;
            int bonesCountOriginal = 0;
            string[] boneNamesOriginal;
            string[] boneNamesMod;
            string[] boneNamesOriginalFiltered;
            string[] bonePathIDsMod;
            string m_RootBonePathID;
            int boneIndex;
            string boneIndexText;
            string data2;
            string dataBindArray;

            string[] drops = (string[])e.Data.GetData(DataFormats.FileDrop);
            if(drops.Length != 3)
            {
                MessageBox.Show("Drop exactly one file and two folders");
                return;
            }
            path_root = Path.GetDirectoryName(drops[0]);
            foreach(string path in drops)
            {
                if (File.GetAttributes(path).HasFlag(FileAttributes.Directory))
                {
                    if(Directory.GetFiles(path, "hd_avatar_parts_*.txt").FirstOrDefault() != null)
                    {
                        path_unity3d = path;
                    }
                    if (Directory.GetFiles(path, "*level0*.txt").FirstOrDefault() != null)
                    {
                        path_level0 = path;
                    }
                }
                else
                {
                    if (Path.GetFileNameWithoutExtension(path).Contains("-sharedassets0.assets") && Path.GetFileName(path).EndsWith("Mesh.txt"))
                    {
                        path_mesh = path;
                        meshName = Path.GetFileNameWithoutExtension(path).Substring(0, Path.GetFileNameWithoutExtension(path).IndexOf("-sharedassets0.assets"));
                    }
                }
            }
            if(path_mesh == "")
            {
                MessageBox.Show("Dump of the mesh from the sharedassets0.assets file not found");
                return;
            }
            if (path_level0 == "")
            {
                MessageBox.Show("Dump folder of the level0 file not found");
                return;
            }
            if (path_unity3d == "")
            {
                MessageBox.Show("Dump folder of the original unity3d file not found");
                return;
            }

            file = Directory.GetFiles(path_level0, meshName + "*GameObject.txt").FirstOrDefault();
            if (file == null)
            {
                MessageBox.Show("level0 GameObject not found");
                return;
            }
            data = File.ReadAllText(file);
            pos = data.IndexOf("[1]");
            pos = data.IndexOf("m_PathID", pos) + 11;
            len = data.IndexOf("\r\n", pos) - pos;
            m_PathID = data.Substring(pos, len);

            file = Directory.GetFiles(path_level0, "*-" + m_PathID + "-SkinnedMeshRenderer.txt").FirstOrDefault();
            if (file == null)
            {
                MessageBox.Show("level0 SMR not found");
                return;
            }

            data = File.ReadAllText(file);
            pos = data.IndexOf("vector m_Bones");
            pos = data.IndexOf("int size", pos) + 11;
            len = data.IndexOf("\r\n", pos) - pos;
            bonesCount = int.Parse(data.Substring(pos, len));
            bonePathIDsMod = new string[bonesCount];
            boneNamesMod = new string[bonesCount];
            for (int i = 0; i < bonesCount; i++)
            {
                pos = data.IndexOf("m_PathID", pos) + 11;
                len = data.IndexOf("\r\n", pos) - pos;
                bonePathIDsMod[i] = data.Substring(pos, len);
            }
            pos = data.IndexOf("m_RootBone", pos);
            pos = data.IndexOf("m_PathID", pos) + 11;
            len = data.IndexOf("\r\n", pos) - pos;
            m_RootBonePathID = data.Substring(pos, len);


            for (int i = 0; i < bonesCount; i++)
            {
                file = Directory.GetFiles(path_level0, "*-" + bonePathIDsMod[i] + "-Transform.txt").FirstOrDefault();
                if (file == null)
                {
                    MessageBox.Show("level0 " + bonePathIDsMod[i] + "transform not found");
                    return;
                }
                data = File.ReadAllText(file);
                pos = data.IndexOf("m_GameObject");
                pos = data.IndexOf("m_PathID", pos) + 11;
                len = data.IndexOf("\r\n", pos) - pos;
                m_PathID = data.Substring(pos, len);

                file = Directory.GetFiles(path_level0, "*-" + m_PathID + "-GameObject.txt").FirstOrDefault();
                if (file == null)
                {
                    MessageBox.Show("level0 " + m_PathID + "GameObject not found");
                    return;
                }
                data = File.ReadAllText(file);
                pos = data.IndexOf("string m_Name") + 17;
                len = data.IndexOf("\"", pos) - pos;
                boneNamesMod[i] = data.Substring(pos, len);



            }

            file = Directory.GetFiles(path_unity3d, meshName + "*Mesh.txt").FirstOrDefault();
            if (file == null)
            {
                MessageBox.Show("original mesh not found");
                return;
            }
            meshFilePath = file;

            file = Directory.GetFiles(path_unity3d, meshName + "*GameObject.txt").FirstOrDefault();
            if (file == null)
            {
                MessageBox.Show("original GameObject not found");
                return;
            }
            data = File.ReadAllText(file);
            pos = data.IndexOf("[2]");
            pos = data.IndexOf("m_PathID", pos) + 11;
            len = data.IndexOf("\r\n", pos) - pos;
            m_PathID = data.Substring(pos, len);
            if (m_PathID.StartsWith("-"))
            {
                m_PathID = m_PathID.Trim('-');
                ulong d_PathID = ulong.Parse(m_PathID);
                d_PathID = ulong.MaxValue - d_PathID + 1;
                m_PathID = d_PathID.ToString();
            }

            file = Directory.GetFiles(path_unity3d, "*-" + m_PathID + "-MonoBehaviour.txt").FirstOrDefault();
            if (file == null)
            {
                MessageBox.Show("original MonoBehaviour not found");
                return;
            }
            monoPath = file;
            data = File.ReadAllText(file);
            pos = data.IndexOf("vector boneNames_");
            pos = data.IndexOf("int size", pos) + 11;
            len = data.IndexOf("\r\n", pos) - pos;
            bonesCountOriginal = int.Parse(data.Substring(pos, len));



            boneNamesOriginal = new string[bonesCountOriginal];
            for (int i = 0; i < bonesCountOriginal; i++)
            {
                pos = data.IndexOf("string data", pos) + 15;
                len = data.IndexOf("\"", pos) - pos;
                boneNamesOriginal[i] = data.Substring(pos, len);
            }

            if (bonesCountOriginal == bonesCount)
            {
                boneNamesOriginalFiltered = boneNamesOriginal;
            }
            else if (bonesCountOriginal > bonesCount)
            {
                boneNamesOriginalFiltered = new string[bonesCount];
                boneNamesOriginalFiltered = boneNamesOriginal.Where(val => boneNamesMod.Contains(val)).ToArray();
            }
            else
            {
                MessageBox.Show("Bone count does not match");
                return;
            }

            /*file = Directory.GetFiles(path_level0, meshName + "*Mesh.txt").FirstOrDefault();
            if (file == null)
            {
                MessageBox.Show("mod mesh not found");
                return;
            }*/
            meshData = File.ReadAllLines(path_mesh);
            count = meshData.Length;
            meshDataNew = new string[count];
            pos = 0;

            while (!meshData[pos].Contains("m_BoneNameHashes"))
            {
                meshDataNew[pos] = meshData[pos];
                pos++;
            }

            meshDataNew[pos] = meshData[pos];
            pos++;
            meshDataNew[pos] = meshData[pos];
            pos++;
            meshDataNew[pos] = meshData[pos];
            pos++;
            pos0 = pos;
            for (int i = 0; i < bonesCount; i++)
            {
                boneIndex = Array.IndexOf(boneNamesOriginalFiltered, boneNamesMod[i]);

                if (boneIndex == -1)
                {
                    MessageBox.Show("skeletons does not match");
                    return;
                }

                meshDataNew[pos] = meshData[pos];
                pos++;
                meshDataNew[pos] = meshData[pos0 + boneIndex * 2 + 1];
                pos++;
            }

            while (pos < count)
            {
                if (meshData[pos].Contains("boneIndex"))
                {
                    boneIndexText = meshData[pos].Substring(meshData[pos].IndexOf("boneIndex") + 15);
                    boneIndex = int.Parse(boneIndexText);
                    boneIndex = Array.IndexOf(boneNamesOriginalFiltered, boneNamesMod[boneIndex]);
                    meshDataNew[pos] = meshData[pos].Replace("= " + boneIndexText, "= " + boneIndex.ToString());
                    pos++;
                }
                else
                {
                    meshDataNew[pos] = meshData[pos];
                    pos++;
                }
            }

            File.WriteAllLines(meshFilePath, meshDataNew);

            data2 = File.ReadAllText(meshFilePath);

            pos = data.IndexOf("vector bindPoses_");
            pos = data.IndexOf("0 Array Array", pos);
            dataBindArray = data.Substring(pos);

            string boneNames_Array;
            pos = data.IndexOf("vector boneNames_");
            pos = data.IndexOf("0 Array Array", pos);
            len = data.IndexOf(" 0 PPtr<$SkinnedMeshRenderer> skin_", pos) - pos;
            boneNames_Array = data.Substring(pos, len);

            List<string> bindPoses = new List<string>();
            List<string> boneNames_ = new List<string>();

            if (bonesCountOriginal > bonesCount)
            {
                pos = 0;
                count = 0;
                while (pos < dataBindArray.Length && count < bonesCountOriginal)
                {
                    pos = dataBindArray.IndexOf("]", pos);
                    len = dataBindArray.IndexOf("[", pos) - pos;
                    if (len < 0)
                    {
                        len = dataBindArray.Length - pos;
                    }
                    if (Array.IndexOf(boneNamesOriginalFiltered, boneNamesOriginal[count]) != -1)
                    {
                        bindPoses.Add(dataBindArray.Substring(pos, len));
                    }

                    count++;
                    pos++;
                }

                dataBindArray = dataBindArray.Substring(0, dataBindArray.IndexOf("[")).Replace(bonesCountOriginal.ToString(), bonesCount.ToString());
                for (int i = 0; i < bonesCount; i++)
                {
                    dataBindArray += "[" + i.ToString() + bindPoses[i];
                }
                dataBindArray = dataBindArray.TrimEnd(' ');

                pos = data.IndexOf("vector bindPoses_");
                pos = data.IndexOf("0 Array Array", pos);
                data = data.Substring(0, pos) + dataBindArray;


                pos = 0;
                count = 0;
                while (pos < boneNames_Array.Length && count < bonesCountOriginal)
                {
                    pos = boneNames_Array.IndexOf("]", pos);
                    len = boneNames_Array.IndexOf("[", pos) - pos;
                    if (len < 0)
                    {
                        len = boneNames_Array.Length - pos;
                    }
                    if (Array.IndexOf(boneNamesOriginalFiltered, boneNamesOriginal[count]) != -1)
                    {
                        boneNames_.Add(boneNames_Array.Substring(pos, len));
                    }

                    count++;
                    pos++;
                }

                boneNames_Array = boneNames_Array.Substring(0, boneNames_Array.IndexOf("[")).Replace(bonesCountOriginal.ToString(), bonesCount.ToString());
                for (int i = 0; i < bonesCount; i++)
                {
                    boneNames_Array += "[" + i.ToString() + boneNames_[i];
                }
                boneNames_Array = boneNames_Array.TrimEnd(' ');
                pos = data.IndexOf("vector boneNames_");
                pos = data.IndexOf("0 Array Array", pos);
                len = data.IndexOf(" 0 PPtr<$SkinnedMeshRenderer> skin_", pos) - pos;
                data = data.Substring(0, pos) + boneNames_Array + data.Substring(pos + len);
            }


            pos = data2.IndexOf("vector m_BindPose");
            pos = data2.IndexOf("0 Array Array", pos);
            len = data2.IndexOf(" 0 vector m_BoneNameHashes", pos) - pos;
            data2 = data2.Substring(0, pos) + dataBindArray + data2.Substring(pos + len);
            File.WriteAllText(meshFilePath, data2);

            pos = data2.IndexOf("vector m_Skin");
            pos = data2.IndexOf("0 Array Array", pos);
            len = data2.IndexOf(" 1 VertexData m_VertexData", pos) - pos;
            data2 = data2.Substring(pos, len);


            pos = data.IndexOf("originalBoneWeights_");
            pos = data.IndexOf("0 Array Array", pos);
            len = data.IndexOf(" 0 vector bindPoses_", pos) - pos;

            data = data.Substring(0, pos) + data2 + data.Substring(pos + len);
            File.WriteAllText(monoPath, data);

            monoData = File.ReadAllLines(monoPath);

            pos = 0;
            count = monoData.Length;
            while (pos < count)
            {
                if (monoData[pos].Contains("BoneInfluence"))
                {
                    buffer[0] = monoData[pos + 1];
                    buffer[1] = monoData[pos + 2];
                    buffer[2] = monoData[pos + 3];
                    buffer[3] = monoData[pos + 4];

                    monoData[pos] = monoData[pos].Replace("BoneInfluence", "BoneWeight");
                    monoData[pos + 1] = monoData[pos + 5].Replace("[", "").Replace("]", "");
                    monoData[pos + 2] = monoData[pos + 6].Replace("[", "").Replace("]", "");
                    monoData[pos + 3] = monoData[pos + 7].Replace("[", "").Replace("]", "");
                    monoData[pos + 4] = monoData[pos + 8].Replace("[", "").Replace("]", "");
                    monoData[pos + 5] = buffer[0].Replace("[", "").Replace("]", "");
                    monoData[pos + 6] = buffer[1].Replace("[", "").Replace("]", "");
                    monoData[pos + 7] = buffer[2].Replace("[", "").Replace("]", "");
                    monoData[pos + 8] = buffer[3].Replace("[", "").Replace("]", "");
                    pos += 9;
                }
                pos++;
            }
            File.WriteAllLines(monoPath, monoData);
            /*
            File.WriteAllLines(@"k:\Mega\Links\alice gear aegis modding\kaede\machi\bonesMod.txt", boneNamesMod);
            File.WriteAllLines(@"k:\Mega\Links\alice gear aegis modding\kaede\machi\bonesOriginal.txt", boneNamesOriginal);
            File.WriteAllLines(@"k:\Mega\Links\alice gear aegis modding\kaede\machi\bonesOriginalFiltered.txt", boneNamesOriginalFiltered);
            Array.Sort(boneNamesMod);
            Array.Sort(boneNamesOriginal);
            Array.Sort(boneNamesOriginalFiltered);
            File.WriteAllLines(@"k:\Mega\Links\alice gear aegis modding\kaede\machi\bonesModSorted.txt", boneNamesMod);
            File.WriteAllLines(@"k:\Mega\Links\alice gear aegis modding\kaede\machi\bonesOriginalSorted.txt", boneNamesOriginal);
            File.WriteAllLines(@"k:\Mega\Links\alice gear aegis modding\kaede\machi\bonesOriginalFilteredSorted.txt", boneNamesOriginalFiltered);
            */
            MessageBox.Show("Completed");

        }

        private void label_boneOrderFix_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void button_deobfuscate_Click(object sender, EventArgs e)
        {
            string afpPath = gamePath + "\\alice_main_Data\\Managed\\Assembly-CSharp-firstpass.dll";
            string bfPath = gamePath + "\\alice_main_Data\\Managed\\BlowFishCS.dll";
            string[] dlls = new string[3] { assemblyPath, afpPath, bfPath };
            byte[] data;
            byte[] header = new byte[10] { 0xB2, 0xA5, 0x6F, 0xFF, 0xFC, 0xFF, 0xFF, 0xFF, 0xFB, 0xFF };
            byte[] dllheader = new byte[10] { 0x4D, 0x5A, 0x90, 0x00, 0x03, 0x00, 0x00, 0x00, 0x04, 0x00 };

            foreach(string dll in dlls)
            {
                if (File.Exists(dll))
                {
                    data = File.ReadAllBytes(dll);
                    Array.Copy(dllheader, 0, data, 0, 10);
                    File.WriteAllBytes(dll, data);
                }
            }
        }

        private void button_obfuscate_Click(object sender, EventArgs e)
        {
            string afpPath = gamePath + "\\alice_main_Data\\Managed\\Assembly-CSharp-firstpass.dll";
            string bfPath = gamePath + "\\alice_main_Data\\Managed\\BlowFishCS.dll";
            string[] dlls = new string[3] { assemblyPath, afpPath, bfPath };
            byte[] data;
            byte[] header = new byte[10] { 0xB2, 0xA5, 0x6F, 0xFF, 0xFC, 0xFF, 0xFF, 0xFF, 0xFB, 0xFF };
            byte[] dllheader = new byte[10] { 0x4D, 0x5A, 0x90, 0x00, 0x03, 0x00, 0x00, 0x00, 0x04, 0x00 };

            foreach (string dll in dlls)
            {
                if (File.Exists(dll))
                {
                    data = File.ReadAllBytes(dll);
                    Array.Copy(header, 0, data, 0, 10);
                    File.WriteAllBytes(dll, data);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileStream fs1 = File.OpenRead(@"d:\Games\DMM\aga android\rev.24295");
           

            
            using (FileStream fs2 = File.Create(@"d:\Games\DMM\aga android\rev.txt"))
            {
                using (DeflateStream decompressionStream = new DeflateStream(fs1, CompressionMode.Decompress))
                {
                    decompressionStream.CopyTo(fs2);
                }
            }
        }

        public class info
        {
            public string download { get; set; }
            public string crc { get; set; }
            public string hash { get; set; }
            public int size { get; set; }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            /*
            byte[] common = File.ReadAllBytes(@"d:\Mega\Links\alice gear aegis modding\_database\common.24437");
            byte[] Key = new byte[32] { 0x10, 0x70, 0xab, 0x1b, 0x43, 0x87, 0x4f, 0xc0, 0xa1, 0x9e, 0xc4, 0x1d, 0x62, 0x94, 0x14, 0x32, 0xc5, 0x3a, 0xa7, 0xdb, 0x48, 0x3f, 0x57, 0x89, 0x68, 0x15, 0x7a, 0xf2, 0x40, 0x8f, 0x11, 0x4d };
            byte[] IV = new byte[16] { 0xfa, 0x47, 0x1b, 0xba, 0x9a, 0xfa, 0xe0, 0xd8, 0x7c, 0x49, 0x9b, 0xb0, 0xa8, 0xa8, 0xc9, 0x61 };

            Aes aesAlg = Aes.Create();
            aesAlg.Key = Key;
            aesAlg.IV = IV;
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            MemoryStream msDecrypt = new MemoryStream(common);
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write);
            csDecrypt.Write(common, 0, common.Length);
            csDecrypt.Close();
            
            byte[] clearBytes = msDecrypt.ToArray();
            File.WriteAllBytes(@"d:\Mega\Links\alice gear aegis modding\_database\common3.gzip", clearBytes);


          
            
            var msi = new MemoryStream(clearBytes);
            var mso = new MemoryStream();
            var gs = new GZipStream(msi, CompressionMode.Decompress);
            gs.CopyTo(mso);
            string res = Encoding.UTF8.GetString(mso.ToArray());

            File.WriteAllText(@"d:\Mega\Links\alice gear aegis modding\_database\common2.json", res);

            */


            js.MaxJsonLength = int.MaxValue;
            string url;
            string saveto = @"d:\_DEL\aga data\";
            string json = File.ReadAllText(@"d:\Mega\Links\alice gear aegis modding\_database\common2.json");
            Dictionary<string, info> jsondata = (Dictionary<string, info>)js.Deserialize<Dictionary<string, info>>(json);
            int count = 0;
            int total = jsondata.Count;

            Task.Factory.StartNew(() => {
                foreach (string key in jsondata.Keys)
                {
                    url = @"https://i-cf.alice.colopl.jp/common/pc/" + key + "." + jsondata[key].hash + "?h=" + jsondata[key].hash;
                    using (var client = new WebClient())
                    {
                        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        client.Headers.Add("user-agent", "AGA");
                        client.DownloadFile(url, saveto + key.Substring(key.LastIndexOf("/")));
                    }
                    count++;
                    button4.Invoke(new Action(() => button4.Text = count.ToString() + "/" + total.ToString()));
                }

            });
            MessageBox.Show("Done");

        }
    }
}
