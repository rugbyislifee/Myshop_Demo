using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        /* 'creando constructor' */
        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        /* 'cuando añaden producto, no es nececario guardar en DB directo
          por eso con ese metodo vamos a guardar primero en CACHE' */
        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }

        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }

        public void Update(ProductCategory ProductCategory)
        {

            /* 'aca estamos buscando producto por su ID y 
                estamos asignando a productToUpdate cual utilisaremos abajo ' */
            ProductCategory productCategoryToUpdate = productCategories.Find(p => p.Id == ProductCategory.Id);

            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = ProductCategory;
            }
            else
            {
                throw new Exception("Product Category Not Found");
            }
        }

        public ProductCategory Find(string Id)
        {
            ProductCategory productCategory = productCategories.Find(p => p.Id == Id);

            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product Category Not Found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        public void Delete(String Id)
        {
            /* 'aca estamos buscando producto por su ID y 
                estamos asignando a productToUpdate cual utilisaremos abajo ' */
            ProductCategory productCategoryToDelete = productCategories.Find(p => p.Id == Id);

            if (productCategoryToDelete != null)
            {
                productCategories.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Product Category Not Found");
            }
        }

    }
}

