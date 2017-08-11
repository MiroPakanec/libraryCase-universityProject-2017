using Case.SystemTest.TestStepLibrary.Element.Selectors;

namespace Case.SystemTest.TestStepLibrary.Element
{
    internal class BaseElement
    {
        internal ClassSelector WithClass(string @class) => new ClassSelector(@class);
        internal NameSelector WithName(string name) => new NameSelector(name);
        internal IdSelector WithId(string id) => new IdSelector(id);
    }
}
