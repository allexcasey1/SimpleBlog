namespace Application.Builders
{

    public class HtmlBuilder
    {
        private readonly string _rootName;
        HtmlElement _root = new HtmlElement();

        public HtmlBuilder(string rootName)
        {
            _rootName = rootName;
            _root._name = _rootName;
        }

        public HtmlBuilder AddChild(string childName, string childText)
        {
            var e = new HtmlElement(childName, childText);

            _root._elements.Add(e);

            return this;
        }
        public HtmlBuilder AddChild(string childName, string childText, List<Attribute> attributes)
        {
            var e = new HtmlElement(childName, childText, attributes);
                
            _root._elements.Add(e);

            return this;
        }

        public override string ToString()
        {
            return _root.ToString();
        }

        public void Clear()
        {
            _root = new HtmlElement();
        }
    }
}