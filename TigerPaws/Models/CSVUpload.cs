using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TigerPaws.Models
{
    public class CSVUpload
    {
        public IEnumerable<Genre> Genres { get; set; }

        public int? Id { get; set; }

        public string Name { get; set; }

        public byte? NumberInStock { get; set; }

        public string Description { get; set; }

        public string GenreName{ get; set; }

        public string Image { get; set; }


        internal static CSVUpload ParseFromCSV(string line)
        {
            var columns = line.Split(',');

            return new CSVUpload
            {
                Id = int.Parse(columns[0]),
                Name = columns[1],
                GenreName = columns[2],
                Description = columns[3],
                NumberInStock = byte.Parse(columns[4])
            };
        }
    }
}