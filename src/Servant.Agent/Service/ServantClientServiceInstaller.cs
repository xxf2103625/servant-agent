﻿using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Servant.Agent.Service
{
    [RunInstaller(true)]
    public class ServantAgentServiceInstaller : Installer
    {
        readonly ServiceProcessInstaller _processInstaller = new ServiceProcessInstaller();
        readonly ServiceInstaller _serviceInstaller = new ServiceInstaller();

        public ServantAgentServiceInstaller()
        {
            _processInstaller.Account = ServiceConfig.AccountType;
            _processInstaller.Username = null;
            _processInstaller.Password = null;
            _serviceInstaller.DisplayName = ServiceConfig.DisplayName;
            _serviceInstaller.ServiceName = ServiceConfig.ServiceName;
            _serviceInstaller.Description = ServiceConfig.Description;
            _serviceInstaller.StartType = ServiceConfig.StartType;

            Installers.Add(_processInstaller);
            Installers.Add(_serviceInstaller);
        }

        protected override void OnAfterInstall(IDictionary savedState)
        {
            base.OnAfterInstall(savedState);

            var serviceController = new ServiceController(ServiceConfig.ServiceName);
            serviceController.Start();
        }
    }
}