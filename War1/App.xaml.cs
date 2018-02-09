using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using War1.Units;
using War1.WarBasics;
using War1.WarBasics.Abilities;
using War1.WarBasics.Map;
using Xamarin.Forms;

namespace War1
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new War1.MainPage();
		}

		protected override void OnStart ()
		{
            Archer archer1 = new Archer() { Position = new MapPosition(5, 20) };
        }

        protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
