using System;
using OOPASU;
using OOPASU.Domain;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1
{
    [TestClass]
    public class CrudTest
    {
        [TestMethod]
        public async void AddTest()
        {
            var testHelper = new TestHelper();
            var context = testHelper.Context;
            var repository = testHelper.IntellegentWorkRepository;
            var intellegentWork = new IntellegentWork
            {
                Id = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4"),
                Title = "testTitle",
                Category = "testCategory",
                Description = "testDescription",
                GRNTI = "testGRNTI",
                DOI = "testDOI",
                Place = "testPlace",
                Year = 2000,
                Status = "testStatus"
            };

            //var IntellegentWork = context.IntellegentWorks.Find(new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4"));
            intellegentWork.KeyWords.Add(new KeyWord { Word = "KeyWord1" } );
            intellegentWork.KeyWords.Add(new KeyWord { Word = "KeyWord2" } );
            repository.AddAsync(intellegentWork).Wait();
            var work = await repository.GetByIdAsync(new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4"));

            Assert.AreEqual(1, work.KeyWords.Where(w => w.Word == "KeyWord1").Count());
            Assert.AreEqual(1, work.KeyWords.Where(w => w.Word == "KeyWord2").Count());
            Assert.AreEqual("testTitle", work.Title);
            Assert.AreEqual(2000, work.Year);
            Assert.AreEqual("testPlace", work.Place);

            context.SaveChanges();
        }
        [TestMethod]
        public async void FindTest()
        {
            var testHelper = new TestHelper();
            var context = testHelper.Context;

            var intellegentWork1 = new IntellegentWork
            {
                Id = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E5"),
                Title = "testTitle",
                Category = "testCategory",
                Description = "testDescription",
                GRNTI = "testGRNTI",
                DOI = "testDOI",
                Place = "testPlace",
                Year = 2000,
                Status = "testStatus"
            };
            var intellegentWork2 = new IntellegentWork
            {
                Id = new Guid("E9168C5E-CEB2-4faa-B6BF-329BF39FA1E5"),
                Title = "testTitle2",
                Category = "testCategory2",
                Description = "testDescription2",
                GRNTI = "testGRNTI2",
                DOI = "testDOI2",
                Place = "testPlace2",
                Year = 2001,
                Status = "testStatus2"
            };
            intellegentWork1.KeyWords.Add(new KeyWord { Word = "KeyWord1" });
            intellegentWork2.KeyWords.Add(new KeyWord { Word = "KeyWord2" });

            context.IntellegentWorks.Add(intellegentWork1);
            context.IntellegentWorks.Add(intellegentWork2);
            context.SaveChanges();


            /*var repository = testHelper.IntellegentWorkRepository;
            repository.AddAsync(intellegentWork1).Wait();
            repository.AddAsync(intellegentWork2).Wait();

            var work = await repository.GetByIdAsync(new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4"));

            Assert.AreEqual(1, work.KeyWords.Where(w => w.Word == "KeyWord1").Count());

            var iw1 = context.IntellegentWorks.Where(r => r.KeyWords.Word == "KeyWord1").FirstOrDefault();
            var iw2 = context.IntellegentWorks.Where(r => r.KeyWords.Word == "KeyWord2").FirstOrDefault();

            Assert.AreEqual(new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E5"), iw1.Id);
            Assert.AreEqual(new Guid("E9168C5E-CEB2-4faa-B6BF-329BF39FA1E5"), iw2.Id);*/
        }
    }
}
