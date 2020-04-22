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

namespace AGA_Helper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            gamePath = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\DMM GAMES\\Launcher\\Content\\alicegearaegisexe", "Path", "");
            if (gamePath == "")
            {
                gamePath = AppDomain.CurrentDomain.BaseDirectory;
            }

            label_gamepath.Text = gamePath;
            assemblyPath = gamePath + "\\alice_main_Data\\Managed\\Assembly-CSharp.dll";
            enginePath = gamePath + "\\alice_main_Data\\Managed\\UnityEngine.dll";

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

            ascs = File.ReadAllBytes(assemblyPath);

            if (GetPosition(ascs, pattern) >= 0)
            {
                button_patch.Text = "Uncensor";
                button_restore.Text = "Censored";
                button_patch.ForeColor = System.Drawing.Color.Black;
                button_restore.ForeColor = System.Drawing.Color.Red;
            }
            else if (GetPosition(ascs, patternUncensored) >= 0)
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
                MessageBox.Show("Uncensor code needs updating");
            }
            
        }

        string gamePath;
        string assemblyPath;
        string enginePath;
        FileStream fsa;
        FileStream fsu;
        byte[] ascs;
        byte[] pattern = new byte[] { 0x00, 0x06, 0x2a, 0x00, 0x13, 0x30, 0x09, 0x00, 0x9a }; 
        byte[] patternUncensored = new byte[] { 0x00, 0x06, 0x2a, 0x00, 0x0a, 0x16, 0x2a, 0x00, 0x9a };
        byte[] decensor = new byte[] { 0x0a, 0x16, 0x2a };
        byte[] censor = new byte[] { 0x13, 0x30, 0x09 };
        int pos;
        string md5Patched = "790955bb32ce23b3a43c5be6b52fa943";


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
                pos = GetPosition(ascs, pattern);
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
                pos = GetPosition(ascs, patternUncensored);
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

        int GetPosition(byte[] data, byte[] pattern)
        {
            for (int i = 0; i < data.Length - pattern.Length; i++)
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
            button_patch.Invoke(new Action(() => button_patch.Enabled = false));
            button_restore.Invoke(new Action(() => button_restore.Enabled = false));
            button_patchTranslation.Invoke(new Action(() => button_patchTranslation.Enabled = false));
            button_updateTranslation.Invoke(new Action(() => button_updateTranslation.Enabled = false));
            button_permaLock.Invoke(new Action(() => button_permaLock.Enabled = false));
        }

        public void enableControls()
        {
            button_launch.Invoke(new Action(() => button_launch.Enabled = true));
            checkBox_lock_a.Invoke(new Action(() => checkBox_lock_a.Enabled = true));
            checkBox_lock_u.Invoke(new Action(() => checkBox_lock_u.Enabled = true));
            button_patch.Invoke(new Action(() => button_patch.Enabled = true));
            button_restore.Invoke(new Action(() => button_restore.Enabled = true));
            button_patchTranslation.Invoke(new Action(() => button_patchTranslation.Enabled = true));
            button_updateTranslation.Invoke(new Action(() => button_updateTranslation.Enabled = true));
            button_permaLock.Invoke(new Action(() => button_permaLock.Enabled = true));
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
                        MessageBox.Show("Remove write protection from UnityEngine.dll first");
                        return;
                    }
                    disableControls();
                    using (var client = new WebClient())
                    {
                        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        client.Headers.Add("user-agent", "AGA");
                        client.DownloadFile("https://github.com/Aceship/Alice-Gear-Aegis-EN-Text/releases/download/0.10/translation_patch.zip", gamePath + "\\translation_patch.zip");
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
                    client.DownloadFile("https://raw.githubusercontent.com/Globalnet626/random/master/takeownership.bat", gamePath + "\\alice_main_Data\\Managed\\takeownership.bat");
                }
                if (!File.Exists(gamePath + "\\alice_main_Data\\Managed\\takeownership.bat"))
                {
                    MessageBox.Show("Couldn't download takeownership.bat");
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
                enableControls();
            });
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"AGA Helper v0.2

    Includes:
    Uncensor
    Translation patch
    Patched files locking
    Launcher

    Game path is detected by registry.If for some reason this doesn't work, try to run utility from game folder or with admin privileges.

    Use `Uncensor` button to remove Access Denied censorship - this will modify Assembly - CSharp.dll.

    Use `Patch` button to download and apply translation patch - this will modify UnityEngine.dll.

    Use `Get Latest Translation` button to download and apply latest translation files.

    If running the game through the `Launch` button, use checkboxes to protect patched Assembly - CSharp.dll(uncensor) and UnityEngine.dll(translation) from updating by DMM client.
    During game updates, uncheck `Lock Assembly - CSharp.dll` to allow its updating, and reapply uncensor afterwards.

    Or use `Permanent Lock / Unlock` button to lock files by setting file permissions for UnityEngine and Assembly-CSharp, and run game as usual through DMM client.
    This needs administrator privileges, and may not work on non - English Windows versions.

    Translation patch is made using https://github.com/bbepis/XUnity.AutoTranslator 

    Translation is downloaded from https://github.com/Aceship/Alice-Gear-Aegis-EN-Text

    Permanent Lock / Unlock batch is downloaded from https://github.com/Globalnet626/random/blob/master/takeownership.bat");

        }
    }
}
