using System;
using System.Data.Entity;
using System.Reflection;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Web.Configuration;
using AccountingBook.Models;
using AccountingBook.Repository;
using AccountingBook.Repository.Interface;
using AccountingBook.Service;
using AccountingBook.Service.Interface;

namespace AccountingBook.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container

        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        #endregion Unity Container

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            //DbContext
            container.RegisterType<DbContext, AccountingBookModel>(
                new PerRequestLifetimeManager());

            //Unit of Work
            container.RegisterType<IUnitOfWork, EFUnitOfWork>(
                new PerRequestLifetimeManager());

            //Repository
            container.RegisterType(
                typeof(IRepository<>),
                typeof(Repository<>),
                new PerRequestLifetimeManager());

            //Service
            container.RegisterType<IAccountBookService, AccountBookService>();
        }
    }
}