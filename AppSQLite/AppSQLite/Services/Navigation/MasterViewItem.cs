using System;

namespace AppSQLite.Services.Navigation
{
    public class MasterViewItem
    {
        public string Title { get; set; }

        public string IconSource { get; set; }

        public Type TargetType { get; set; }
    }
}