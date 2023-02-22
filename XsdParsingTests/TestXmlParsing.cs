using System;
using NUnit.Framework;
using www.nhsia.nhs.uk.DataStandards.XMLschema.CDS.ns;

namespace XsdParsingTests
{
    public class TestXmlParsing
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void TestSavingNewCdsXmlInterchangeElement()
        {
            var element = new CDSXMLInterchange() {
                SchemaDate = DateTime.Parse("11/05/2012 12:00:00 AM"), // a date other than this will throw an error
                SchemaVersion = "6-2-3" // a value other than 6-2-3 will error
            };

            var header = new CDSInterchangeHeader_Structure() {
                CDSInterchangeSenderIdentity = "sender",
                CDSInterchangeReceiverIdentity = "receiver",
                CDSInterchangeControlReference = "ctrl ref",
                CDSInterchangeDateOfPreparation = DateTime.Now,
                CDSInterchangeTimeOfPreparation = DateTime.UtcNow,
                CDSInterchangeApplicationReference = "app1"
            };

            var trailer = new CDSInterchangeTrailer_Structure() {
                CDSInterchangeControlCount = 42,
                CDSInterchangeControlReference = "ctrl ref",
                CDSInterchangeReceiverIdentity = "receiver",
                CDSInterchangeSenderIdentity = "sender"
            };

            element.CDSInterchangeHeader = header;
            element.CDSInterchangeTrailer = trailer;

            var allMsgTypes = new CDSXMLInterchange.CDSNetChangeAllMessageTypesLocalType();
            var bulkGroup = new CDSXMLInterchange.CDSBulkGroup160MessageLocalType();

            // because the schema models these as choices (1 or the other), only the last one set will get written to XML. comment out the 
            // "element.CDSBulkGroup160Message.Add(bulkGroup)" line to get 'CDSNetChangeAllMessageTypes' written to the output XML
            element.CDSNetChangeAllMessageTypes.Add(allMsgTypes);
            element.CDSBulkGroup160Message.Add(bulkGroup);

            element.Save("cds1.xml");
        }

        [Test]
        public void TestReadingCdsXml()
        {
            var cdsxml = CDSXMLInterchange.Load(@"nhsCds_simple_eg.xml");

            Assert.NotNull(cdsxml);

            // demonstrates the use of Fixed values in an XSD: note these values are not even in the XML file
            Assert.AreEqual(cdsxml.SchemaDate, DateTime.Parse("2012-05-11"));
            Assert.AreEqual(cdsxml.SchemaVersion, "6-2-3");
        }
    }
}