using AppSQLite.Entities.Base;
using System;

namespace AppSQLite.Entities
{
    public class Customer : EntityBase
    {
        public DateTime DateBirth { get; set; }

        public int DocumentNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Thumbnail { get; set; } = "http://wp-images.emusic.com/assets/avatars/1316/1b53e3b403575fcb9cfcc990ccc0ebcb-bpthumb.jpg";
    }
}