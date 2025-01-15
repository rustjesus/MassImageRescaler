using System.Drawing.Drawing2D;

namespace MassImageRescaler
{
    public partial class Form1 : Form
    {
        private int width = 1920;
        private int height = 1080;
        private string folderPath;
        private string[] imageExtensions;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Define supported image file extensions.
            imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
            supportLabel4.Text = "Supported File Types: ";
            for (int i = 0; i < imageExtensions.Length; i++)
            {
                supportLabel4.Text += " " + imageExtensions[i];

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!Directory.Exists(folderPath))
            {
                MessageBox.Show("The specified directory does not exist. Please enter a valid path.",
                                "Directory Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Process each file in the directory.
            foreach (string filePath in Directory.GetFiles(folderPath))
            {
                string extension = Path.GetExtension(filePath).ToLower();
                if (Array.Exists(imageExtensions, ext => ext == extension))
                {
                    try
                    {
                        // Load the original image.
                        using (Image originalImage = Image.FromFile(filePath))
                        {
                            // Create a new bitmap with the specified target size.
                            using (Bitmap resizedImage = new Bitmap(width, height))
                            {
                                // Set the resolution of the new image to match the original.
                                resizedImage.SetResolution(originalImage.HorizontalResolution, originalImage.VerticalResolution);

                                // Draw the resized image.
                                using (Graphics graphics = Graphics.FromImage(resizedImage))
                                {
                                    // Set high-quality settings for resizing.
                                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                                    // Draw the original image onto the new bitmap
                                    graphics.DrawImage(originalImage, 0, 0, width, height);
                                }

                                // Save the resized image with a new file name.
                                // This example appends a "_resized" suffix before the file extension.
                                string newFileName = Path.GetFileNameWithoutExtension(filePath) + "_resized_" + width + "x" + height + extension;
                                string newFilePath = Path.Combine(folderPath, newFileName);

                                // Choose a proper format based on the extension.
                                System.Drawing.Imaging.ImageFormat format = GetImageFormat(extension);
                                resizedImage.Save(newFilePath, format);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error processing file '{filePath}': {ex.Message}",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            MessageBox.Show("Image processing complete.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private char ValidateInput(string text, int charIndex, char addedChar)
        {
            // Allow control characters, such as backspace, etc.
            if (char.IsControl(addedChar))
            {
                return addedChar;
            }

            // Block the '-' character.
            if (addedChar == '-')
            {
                return '\0';
            }

            // 1) Allow digits 0-9.
            if (char.IsDigit(addedChar))
            {
                return addedChar;
            }

            // Reject any other characters.
            return '\0';
        }
        // Helper method to determine the image format from a file extension.
        private System.Drawing.Imaging.ImageFormat GetImageFormat(string extension)
        {
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    return System.Drawing.Imaging.ImageFormat.Jpeg;
                case ".png":
                    return System.Drawing.Imaging.ImageFormat.Png;
                case ".bmp":
                    return System.Drawing.Imaging.ImageFormat.Bmp;
                case ".gif":
                    return System.Drawing.Imaging.ImageFormat.Gif;
                default:
                    return System.Drawing.Imaging.ImageFormat.Png;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            folderPath = textBox1.Text;
        }

        private void heightTextBox3_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(heightTextBox3.Text, out int newHeight))
            {
                height = newHeight;
            }
        }

        private void widthTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(widthTextBox2.Text, out int newWidth))
            {
                width = newWidth;
            }
        }

        private void widthTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            int selectionIndex = textBox.SelectionStart;
            char validatedChar = ValidateInput(textBox.Text, selectionIndex, e.KeyChar);
            if (validatedChar == '\0')
            {
                e.Handled = true;
            }
        }

        private void heightTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {

            TextBox textBox = sender as TextBox;
            int selectionIndex = textBox.SelectionStart;
            char validatedChar = ValidateInput(textBox.Text, selectionIndex, e.KeyChar);
            if (validatedChar == '\0')
            {
                e.Handled = true;
            }
        }
    }
}

