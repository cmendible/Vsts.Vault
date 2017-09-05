[![Build status](https://ci.appveyor.com/api/projects/status/e962l6fbp4fn2qeo?svg=true)](https://ci.appveyor.com/project/cmendible/vsts-vault)

# Vsts.Vault

Backup your Team Services Git Repositories with VSTS Vault: A simple console application designed to keep a local copy of all your code.

To use VSTS Vault you’ll need to follow these steps:

### 1. Create Alternate Credentials for your Visual Studio Team Services Account
If you haven’t setup alternate credentials for your [Visual Studio Team Services](https://www.visualstudio.com/en-us/products/visual-studio-team-services-vs.aspx) Account follow the instructions of the Alternate Access Credentials section of the following doc:  [Client Authentication Options](https://www.visualstudio.com/docs/report/analytics/client-authentication-options)

### 2. Configure VSTS.Vault Settings
Modify as needed the following parameters in the appsettings.json file:
  ```json
  {
    "VaultConfiguration": {
        "Username": "[User name as specified in your Team Services Alternate Authentication Credentials]",
        "UserEmail": "[An email required just required by Git pull through LibGit2Sharp]",
        "Password": "[Password (or Token) as specified in your Team Services Security Settings]",
        "Account": "[The name of your Team Services account (Not the full Url)]",
        "TargetFolder": "[Full path of the folder where the copy of your repositories will live]"
    }
  }
  ```
    
For more info checkout my post: [Backup your Team Services Git Repositories with VSTS Vault](https://carlos.mendible.com/2016/06/01/backup-team-services-git-repositories-vsts-vault/)