using System;
using System.IO;
using System.Windows.Forms;

namespace FoltiaRenamer
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            // ディレクトリ選択
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                var files = Directory.GetFiles(dialog.SelectedPath);
                foreach (var file in files)
                {
                    var parser = new FileNameParser();
                    var data = parser.Parse(Path.GetFileName(file));
                    var targetDir = Path.GetDirectoryName(file) + Path.DirectorySeparatorChar + data.Title;
                    if (!Directory.Exists(targetDir))
                    {
                        Directory.CreateDirectory(targetDir);
                    }
                    var creater = new NewFileNameCreator();
                    var newFileName = creater.Create(data);
                    var targetPath = targetDir + Path.DirectorySeparatorChar + newFileName;
                    if (!File.Exists(targetPath))
                    {
                        File.Move(file, targetPath);
                    }
                }
            }
        }
    }
}
