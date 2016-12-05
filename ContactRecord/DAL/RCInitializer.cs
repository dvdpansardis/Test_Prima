using ContactRecord.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactRecord.DAL
{
    public class RCInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CRContext>
    {
        protected override void Seed(CRContext context)
        {
            var products = new List<Product>
            {
            new Product{ProductName="SophiA Gestão de Escolas"},
            new Product{ProductName="SophiA Biblioteca"},
            new Product{ProductName="SophiA Cursos Livres e/ou SophiA Acervo"}
            };

            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();
        }
    }
}