using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoltiaRenamer
{
    public class FileNameParser
    {
        public FileData Parse(string fileName)
        {
            var data = new FileData();

            var words = fileName.Split('_');
            if (words.Count() != 6)
            {
                throw new ArgumentException("Splitted words are not 6.");
            }

            var dateTime = words[0].Split('-');
            data.Date = dateTime[0];
            data.Time = dateTime[1];
            data.Title = ReplaceSpecialCharacter(words[1]);
            data.Number = words[2];
            data.SubTitle = ReplaceSpecialCharacter(words[3]);
            data.Definition = words[4];
            var pidExt = words[5].Split('.');
            data.Pid = pidExt[0];
            data.Extension = '.' + pidExt[1];

            return data;
        }

        private string ReplaceSpecialCharacter(string source)
        {
            return source.Replace('☆', '・').Replace('!', '！').Replace('?', '？');
        }
    }

    public class NewFileNameCreator
    {
        public string Create(FileData data)
        {
            var fileName = data.Title;
            if (data.Number.Length == 1)
            {
                fileName += "_0" + data.Number;
            }
            else
            {
                fileName += "_" + data.Number;
            }
            fileName += "_" + data.SubTitle;
            fileName += "_" + DateTime.Now.Year + data.Date;
            fileName += "_" + data.Pid;
            fileName += data.Extension.ToLower();

            return fileName;
        }
    }

    public class FileData
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string Title { get; set; }
        public string Number { get; set; }
        public string SubTitle { get; set; }
        public string Definition { get; set; }
        public string Pid { get; set; }
        public string Extension { get; set; }
    }
}
