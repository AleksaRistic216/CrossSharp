using CrossSharp.Ui;
using Demos.GitControl.Enums;
using Demos.GitControl.Helpers;
using Demos.GitControl.Models;
using Demos.GitControl.Views;

namespace Demos.GitControl.Forms;

public class MainForm : ModularForm
{
    RepositorySettings _repositorySettings;

    public MainForm()
    {
        Title = "Git Control Demo";
        _repositorySettings = SettingsHelpers.LoadSettings<RepositorySettings>(
            SettingKey.Repositories
        );
        AddPageWithNavigation("Repositories", typeof(ManageRepositoriesView));
    }
}
