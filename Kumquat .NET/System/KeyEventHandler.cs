using System.Windows.Forms;

namespace System
{
    internal class KeyEventHandler
    {
        private Action<object, KeyEventArgs> doSearch;

        public KeyEventHandler(Action<object, KeyEventArgs> doSearch)
        {
            this.doSearch = doSearch;
        }
    }
}