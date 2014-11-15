using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoltiaRenamer;
using NUnit.Framework;

namespace FoltiaRenamerUT
{
    public class TestFileNameParser
    {
        [TestCase("0803-0030_魔法科高校の劣等生_18_九校戦編XI_HD_300047.MP4")]
        public void ParseSampleTitle(string fileName)
        {
            var parser = new FileNameParser();
            var data = parser.Parse(fileName);
            
            Assert.AreEqual("0803", data.Date);
            Assert.AreEqual("0030", data.Time);
            Assert.AreEqual("魔法科高校の劣等生", data.Title);
            Assert.AreEqual("18", data.Number);
            Assert.AreEqual("九校戦編XI", data.SubTitle);
            Assert.AreEqual("HD", data.Definition);
            Assert.AreEqual("300047", data.Pid);
            Assert.AreEqual(".MP4", data.Extension);
        }

        [Test]
        public void CreateSampleFileName()
        {
            const string fileName = "0803-0030_魔法科高校の劣等生_18_九校戦編XI_HD_300047.MP4";
            var parser = new FileNameParser();
            var data = parser.Parse(fileName);

            var creater = new NewFileNameCreator();
            var name = creater.Create(data);
            Assert.AreEqual("魔法科高校の劣等生_18_九校戦編XI_20140803_300047.mp4", name);
        }
    }
}
