using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace DesafioEdCRUD.Controllers
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Books
        {
            public const string GetAll = Base + "/books";

            public const string Update = Base + "/books/{postId}";

            public const string Delete = Base + "/books/{postId}";

            public const string Get = Base + "/books/{postId}";

            public const string Create = Base + "/books";
        }

        public static class Authors
        {
            public const string GetAll = Base + "/authors";

            public const string Update = Base + "/authors/{authorId}";

            public const string Delete = Base + "/authors/{authorId}";

            public const string Get = Base + "/authors/{authorId}";

            public const string Create = Base + "/authors";
        }

        public static class Subjects
        {
            public const string GetAll = Base + "/subjects";

            public const string Update = Base + "/subjects/{subjectId}";

            public const string Delete = Base + "/subjects/{subjectId}";

            public const string Get = Base + "/subjects/{subjectId}";

            public const string Create = Base + "/subjects";
        }


        public static class Identity
        {
            public const string Auth = Base + "/identity/auth";           
        }
    }
}
