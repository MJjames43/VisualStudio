﻿using System;
using GitHub.Exports;
using GitHub.UI;
using GitHub.ViewModels;
using System.ComponentModel.Composition;
using ReactiveUI;
using System.Reactive.Subjects;

namespace GitHub.VisualStudio.UI.Views
{
    public class GenericPullRequestCreationView : SimpleViewUserControl<IPullRequestCreationViewModel, GenericPullRequestCreationView>
    { }

    [ExportView(ViewType = UIViewType.PRCreation)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class PullRequestCreationView : GenericPullRequestCreationView
    {
        readonly Subject<ViewWithData> load = new Subject<ViewWithData>();

        public PullRequestCreationView()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                d(ViewModel.CancelCommand.Subscribe(_ => NotifyCancel()));
                d(ViewModel.CreatePullRequest.Subscribe(_ =>
                {
                    NotifyDone();
                    var v = new ViewWithData(UIControllerFlow.PullRequestList);
                    load.OnNext(v);
                }));
            });
        }
    }
}
