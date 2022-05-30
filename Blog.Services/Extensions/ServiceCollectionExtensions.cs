using Blog.Data.Abstract;
using Blog.Data.Concrete;
using Blog.Data.Concrete.EntityFramework.Contexts;
using Blog.Entities.Concrete;
using Blog.Services.Abstract;
using Blog.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollections)
        {
            serviceCollections.AddDbContext<MyBlogContext>();
            serviceCollections.AddIdentity<User, Role>().AddEntityFrameworkStores<MyBlogContext>();
            serviceCollections.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollections.AddScoped<ICategoryService, CategoryManager>();
            serviceCollections.AddScoped<IArticleService, ArticleManager>();
            return serviceCollections;
        }
    }
}
