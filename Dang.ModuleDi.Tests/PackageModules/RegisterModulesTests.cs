using System;
using System.Reflection;
using Dang.ModuleDi.PackageModules;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dang.ModuleDi.Tests.PackageModules
{
    [TestClass]
    public class RegisterModulesTests
    {
        private IServiceCollection _serviceCollection;

        [TestInitialize]
        public void TestInitialize()
        {
            this._serviceCollection = new ServiceCollection();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            RegisterModules.ClearModules();
        }

        [TestMethod]
        public void AddModule_Should_Throw_If_PackageModule_Parameter_Is_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                RegisterModules.AddModule(null));
        }

        [TestMethod]
        public void AddModule_Should_Throw_If_PackageModule_Parameter_Is_Not_Implementing_BasePackageModule()
        {
            var parameter = typeof(ServiceCollection);
            Assert.ThrowsException<ArgumentNullException>(() =>
                RegisterModules.AddModule(parameter));
        }

        [TestMethod]
        public void AddModules_Should_Throw_If_PackageModule_Collection_Is_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                RegisterModules.AddModules(null));
        }

        [TestMethod]
        public void AddModules_Should_Throw_If_PackageModule_Collection_Is_Empty()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                RegisterModules.AddModules(new Type[0]));
        }

        [TestMethod]
        public void RegisterTypes_Should_Throw_If_ServiceCollection_Is_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                RegisterModules.RegisterTypes(null));
        }

        [DataTestMethod]
        [DataRow(null)]
        public void RegisterTypes_Should_Throw_If_Modules_Is_Null_Or_Empty(Type[] typeArray)
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                RegisterModules.RegisterTypes(this._serviceCollection));
        }

        [TestMethod]
        public void RegisterTypes_Should_Create_And_Add_Dependencies_From_Module()
        {
            RegisterModules.AddModule(typeof(TestPackageModule));

            var serviceCollection = new ServiceCollection();

            RegisterModules.RegisterTypes(serviceCollection);

            Assert.AreEqual(serviceCollection.Count, 1);
        }

        private class TestPackageModule : BasePackageModule
        {
            public override Assembly CurrentAssembly => Assembly.GetExecutingAssembly();
            public override void AddDependencies(IServiceCollection serviceCollection)
            {
                serviceCollection.AddTransient<ITestDependency, TestDependency>();
            }
        }

        private class TestDependency : ITestDependency
        {

        }
        private interface ITestDependency
        {

        }
    }
}