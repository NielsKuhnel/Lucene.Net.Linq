﻿using System;
using System.Linq;
using NUnit.Framework;

namespace Lucene.Net.Linq.Tests.Integration
{
    [TestFixture]
    public class ScalarTests : IntegrationTestBase
    {
        private IQueryable<SampleDocument> documents;

        [SetUp]
        public void AddDocuments()
        {
            AddDocument(new SampleDocument { Name = "c", Scalar = 3, Flag = true, Version = new Version(100, 0, 0) });
            AddDocument(new SampleDocument { Name = "a", Scalar = 1, Version = new Version(20, 0, 0) });
            AddDocument(new SampleDocument { Name = "b", Scalar = 2, Flag = true, Version = new Version(3, 0, 0) });

            documents = provider.AsQueryable<SampleDocument>();
        }

        [Test]
        public void Any()
        {
            Assert.That(documents.Any(), Is.True);
        }

        [Test]
        public void Any_Not()
        {
            Assert.That(documents.Any(d => d.Name == "nonesuch"), Is.False);
        }

        [Test]
        public void Count()
        {
            Assert.That(documents.Count(), Is.EqualTo(3), "Count()");
        }

        [Test]
        public void CountLong()
        {
            Assert.That(documents.LongCount(), Is.EqualTo(3L), "LongCount()");
        }

        [Test]
        public void CountWhere()
        {
            Assert.That(documents.Count(d => d.Flag), Is.EqualTo(2), "Count()");
        }

        [Test]
        public void CountAfterSkip()
        {
            Assert.That(documents.Skip(1).Count(), Is.EqualTo(2), "Skip(1).Count()");
        }
    }
}