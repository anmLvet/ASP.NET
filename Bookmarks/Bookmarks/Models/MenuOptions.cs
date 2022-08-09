using System.Collections.Generic;

namespace Bookmarks.Models {
    public class MenuOptions {
        public const string Menu = "Menu";
        public IList<MenuItem> Items { get; set; }
    }
}