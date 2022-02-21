using System.Text;

namespace Application.Builders
{
    public class HtmlElement 
    {
        public string _name = string.Empty;
        public string _text = string.Empty;
        public List<Attribute> _attributes = new List<Attribute>();
        public List<HtmlElement> _elements { get; set; } = new List<HtmlElement>();
        private const int indentSize = 2;

        public HtmlElement()
        {}

        public HtmlElement(string name, string text)
        {
            _name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            _text = text ?? throw new ArgumentNullException(paramName: nameof(text));
        }

        public HtmlElement(string name, string text, List<Attribute> attributes)
        {
            _name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            _text = text ?? throw new ArgumentNullException(paramName: nameof(text));
            _attributes = attributes;
        }

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new String(' ', indentSize * indent);

            sb.Append($"{i}<{_name}");
            if (_attributes.Count > 0)
            {
                foreach (var attribute in _attributes)
                {
                    sb.Append($"{attribute.Name}=\"{attribute.Value}\"");
                }
            }
            sb.Append(">");
            sb.AppendLine();

            if (!string.IsNullOrWhiteSpace(_text))
            {
                sb.Append(new string(' ', indentSize * (indent + 1)));
                sb.AppendLine(_text);
            }
            foreach (var e in _elements)
            {
                sb.Append(e.ToStringImpl(indent + 1));
            }

            sb.AppendLine($"{i}</{_name}>");
            

            

            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }
}