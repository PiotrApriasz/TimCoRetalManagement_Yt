using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using TRMDesktopUI.ViewModels;

namespace TRMDesktopUI
{
    /// <summary>
    /// Setup caliburn.micro
    /// </summary>
    public class Bootstrapper : BootstrapperBase
    {
        /// <summary>
        /// Handle instantiation of our classes or most of them
        /// </summary>
        private SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
        }

        /// <summary>
        /// Creates simple container instance
        /// </summary>
        protected override void Configure()
        {
            _container.Instance(_container);

            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>();

            GetType().Assembly.GetTypes() //GetTypes - get every type in our entire application
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));
        }

        /// <summary>
        /// Launch Shell view model as base view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnStartup(object sender, StartupEventArgs e) => DisplayRootViewFor<ShellViewModel>();

        /// <summary>
        /// Create instance of any object
        /// </summary>
        /// <param name="service"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        protected override object GetInstance(Type service, string key) => _container.GetInstance(service, key);

        /// <summary>
        /// Get all instances of any object
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        protected override IEnumerable<object> GetAllInstances(Type service) => _container.GetAllInstances(service);

        /// <summary>
        /// Construct objects
        /// </summary>
        /// <param name="instance"></param>
        protected override void BuildUp(object instance) => _container.BuildUp(instance);
    }
}
