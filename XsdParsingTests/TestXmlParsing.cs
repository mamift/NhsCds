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
            var element = new CDSXMLInterchange();

            var header = new CDSInterchangeHeader_Structure() {
                CDSInterchangeSenderIdentity = "sender",
                CDSInterchangeReceiverIdentity = "receiver",
                CDSInterchangeControlReference = "ctrl ref",
                CDSInterchangeDateOfPreparation = DateTime.Now,
                CDSInterchangeTimeOfPreparation = DateTime.UtcNow,
                CDSInterchangeApplicationReference = "app1"
            };

            var trailer = new CDSInterchangeTrailer_Structure() {
                CDSInterchangeControlCount = 0,
                CDSInterchangeControlReference = "ctrl ref",
                CDSInterchangeReceiverIdentity = "receiver",
                CDSInterchangeSenderIdentity = "sender"
            };

            element.CDSInterchangeHeader = header;
            element.CDSInterchangeTrailer = trailer;

            var allMsgTypes = new CDSXMLInterchange.CDSNetChangeAllMessageTypesLocalType();
            var bulkGroup = new CDSXMLInterchange.CDSBulkGroup160MessageLocalType();

            // because these two are choices, only the last one set, will get written to XML. comment out the 
            // "element.CDSBulkGroup160Message.Add(bulkGroup)" line to get 'CDSNetChangeAllMessageTypes' written to the output XML
            element.CDSNetChangeAllMessageTypes.Add(allMsgTypes);
            element.CDSBulkGroup160Message.Add(bulkGroup);

            element.Save("cds1.xml");
        }
    }
}