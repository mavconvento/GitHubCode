using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SMSWindowService.Entity
{
    public class XMLConfig
    {
        public XmlNode SystemSettingsXML { get; set; }
        public XmlNode InstallationSettings { get; set; }
        public String DbConnectionString { get; set; }
    }
}
