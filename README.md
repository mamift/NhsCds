# To build

Download the `LinqToXsdCore` global dotnet tool:

`dotnet tool install -g LinqToXsdCore`

To generate the `CDS-XML_Message_Root-V6-2-3.xsd.config` file:

`linqtoxsd config -e CDS-XML_Message_Root-V6-2-3.xsd`

To generate the C# source code (using the .config file):

`linqtoxsd gen -a CDS-XML_Message_Root-V6-2-3.xsd`

Then debug the tests in the `XsdParsingTests`, there's example code that will allow you see how the LinqToXsd library works.