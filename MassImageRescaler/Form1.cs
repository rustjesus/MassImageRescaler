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
                        // Load the image and immediately create a clone so that file lock is released.
                        Image originalImageClone;
                        using (Image originalImage = Image.FromFile(filePath))
                        {
                            originalImageClone = new Bitmap(originalImage);
                        }

                        // Create a new bitmap with the specified target size.
                        using (Bitmap resizedImage = new Bitmap(width, height))
                        {
                            // Set the resolution of the new image to match the original.
                            resizedImage.SetResolution(originalImageClone.HorizontalResolution, originalImageClone.VerticalResolution);

                            // Draw the resized image.
                            using (Graphics graphics = Graphics.FromImage(resizedImage))
                            {
                                // Set high-quality settings for resizing.
                                graphics.CompositingQuality = CompositingQuality.HighQuality;
                                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                graphics.SmoothingMode = SmoothingMode.HighQuality;
                                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                                // Draw the cloned image onto the new bitmap.
                                graphics.DrawImage(originalImageClone, 0, 0, width, height);
                            }

                            // Determine the proper image format for saving.
                            System.Drawing.Imaging.ImageFormat format = GetImageFormat(extension);

                            if (overrideCheckBox1.Checked)
                            {
                                // Save to a temporary file.
                                string tempFileName = Path.GetFileNameWithoutExtension(filePath) + "_resized_temp" + extension;
                                string tempFilePath = Path.Combine(folderPath, tempFileName);

                                resizedImage.Save(tempFilePath, format);

                                // Now that we've disposed of all image objects and released file locks,
                                // delete the original file and rename the temporary file.
                                File.Delete(filePath);
                                File.Move(tempFilePath, filePath);
                            }
                            else
                            {
                                // Append a suffix to create a new file name.
                                string newFileName = Path.GetFileNameWithoutExtension(filePath)
                                                     + "_resized_" + width + "x" + height + extension;
                                string newFilePath = Path.Combine(folderPath, newFileName);

                                resizedImage.Save(newFilePath, format);
                            }
                        }
                        // Dispose of the clone if it isn’t needed further.
                        originalImageClone.Dispose();
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

        private void overrideCheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

