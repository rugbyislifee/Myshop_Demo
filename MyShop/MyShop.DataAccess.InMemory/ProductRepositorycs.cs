using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepositorycs
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        /* 'creando constructor' */
        public ProductRepositorycs() {
            products = cache["products"] as List<Product>;
            if(products == null)
            {
                products = new List<Product>();
            }
        }

        /* 'cuando añaden producto, no es nececario guardar en DB directo
          por eso con ese metodo vamos a guardar primero en CACHE' */                                
        public void Commit(){
            cache["products"] = products;
        }

        public void Insert(Product p) {
            products.Add(p);
        }

        public void Update(Product product) {

            /* 'aca estamos buscando producto por su ID y 
                estamos asignando a productToUpdate cual utilisaremos abajo ' */
            Product productToUpdate = products.Find(p => p.Id == product.Id);

            if (productToUpdate != null) 
{
                productToUpdate = product;
            }
            else {
                throw new Exception("Product Not Found");
            }
        }

        public Product Find(string Id) {
            Product product = products.Find(p => p.Id == Id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }

        public IQueryable<Product> Collection() {
            return products.AsQueryable();
        }

        public void Delete(String Id) {
            /* 'aca estamos buscando producto por su ID y 
                estamos asignando a productToUpdate cual utilisaremos abajo ' */
            Product productToDelete = products.Find(p => p.Id == Id);

            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }

    }
}
