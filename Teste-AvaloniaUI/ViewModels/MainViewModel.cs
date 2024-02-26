using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Teste_AvaloniaUI.Models;
using Teste_AvaloniaUI.Utils;

namespace Teste_AvaloniaUI.ViewModels;

public class MainViewModel : ViewModelBase
{
	private Thread ThreadMonitor;
	private Monitoramento _monitoramento;

	public MainViewModel()
	{
		_monitoramento = new Monitoramento();

		// Alguns apps pra teste...
		_monitoramento.AppsMonitored.Add("Discord", 0);
		_monitoramento.AppsMonitored.Add("Revit", 0);
		_monitoramento.AppsMonitored.Add("Edge", 0);

		ThreadMonitor = new Thread(Monitor);
		ThreadMonitor.Start();
	}
	private void Monitor()
	{
		while (true)
		{
			// Aqui as variáveis estão locais mas é interessante estarem na model pra poder acessar de qualquer parte caso precise
			IntPtr currentWindow = NativeWindowsCalls.GetForegroundWindow();
			StringBuilder windowTitle = new StringBuilder(256);
			NativeWindowsCalls.GetWindowText(currentWindow, windowTitle, windowTitle.Capacity);

			// Caso seja uma janela criada dentro de uma aplicação, também precisamos contar o tempo no app
			// TODO: Retirar isso daqui também
			uint processId;
			NativeWindowsCalls.GetWindowThreadProcessId(currentWindow, out processId);
			Process process = Process.GetProcessById((int)processId);

			ActiveUsingApp = string.Format("{0} - {1}", windowTitle.ToString(), process?.ProcessName);

			// Separa a string pois o programa pode conter vários nomes na janela (Discord - Sala x bla bla)
			string[] splittedString = windowTitle.ToString().Split(' ');

			// Depois de separar a string verifica se ela tá na lista para monitoramento
			// Também verifica se o processo pai está na lista de monitoramento
			IsAppMonitored = false;
			foreach (var item in splittedString)
			{
				if (AppsMonitored.ContainsKey(item))
				{
					AppsMonitored[item]++;
					CycleCount = AppsMonitored[item];
					IsAppMonitored = true;
				}
				else if (process != null && AppsMonitored.ContainsKey(process.ProcessName))
				{
					AppsMonitored[process.ProcessName]++;
					CycleCount = AppsMonitored[process.ProcessName];
					IsAppMonitored = true;
				}

				if (IsAppMonitored)
					break;
			}

			// Reseta se não for um app da lista
			if(!IsAppMonitored)
			{
				CycleCount = 0;
				IsAppMonitored = false;
			}

			Thread.Sleep(1000);
		}
	}

	#region Membros
	public string ActiveUsingApp
	{
		get { return _monitoramento.ActiveUsingApp; }
		set
		{
			if (_monitoramento.ActiveUsingApp != value)
			{
				_monitoramento.ActiveUsingApp = value;
				this.RaisePropertyChanged();
			}
		}
	}

	public bool IsAppMonitored
	{
		get { return _monitoramento.IsAppMonitored; }
		set
		{
			if (_monitoramento.IsAppMonitored != value)
			{
				_monitoramento.IsAppMonitored = value;
				this.RaisePropertyChanged();
			}
		}
	}

	public int CycleCount
	{
		get { return _monitoramento.CycleCount; }
		set
		{
			if (_monitoramento.CycleCount != value)
			{
				_monitoramento.CycleCount = value;
				this.RaisePropertyChanged();
			}
		}
	}

	public Dictionary<string, int> AppsMonitored
	{
		get { return _monitoramento.AppsMonitored; }
		set
		{
			if (_monitoramento.AppsMonitored != value)
			{
				_monitoramento.AppsMonitored = value;
				this.RaisePropertyChanged();
			}
		}
	}
	#endregion
}
