using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace DesafioEdCRUD.Options
{
    public class SwaggerOptions
    {
        public string JsonRoute { get; set; }

        public string Description { get; set; }

        public string UiEndpoint { get; set; }
    }
}
