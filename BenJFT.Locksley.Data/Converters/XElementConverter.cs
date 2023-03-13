using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BenJFT.Locksley.Data.Converters;

public class XElementConverter : ValueConverter<XElement, string> {
    public XElementConverter() : base(v => v.ToString(), s => XElement.Parse(s)) { }
}