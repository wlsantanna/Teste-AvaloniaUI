using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste_AvaloniaUI.Models;

public class Monitoramento
{
	private string _activeUsingApp = string.Empty;
	public string ActiveUsingApp
	{
		get => _activeUsingApp;
		set
		{
			if (_activeUsingApp != value)
			{
				_activeUsingApp = value;
			}
		}
	}

	private bool _isAppMonitored = false;
	public bool IsAppMonitored
	{
		get => _isAppMonitored;
		set
		{
			if (_isAppMonitored != value)
			{
				_isAppMonitored = value;
			}
		}
	}

	// Vai mostrar os segundos utilizados das aplicações monitoradas lá na view
	private int _cycleCount = 0;
	public int CycleCount
	{
		get => _cycleCount;
		set
		{
			if (_cycleCount != value)
			{
				_cycleCount = value;
			}
		}
	}

	// A lista dos aplicativos monitorados
	private Dictionary<string, int> _appsMonitored = new Dictionary<string, int>();
	public Dictionary<string, int> AppsMonitored
	{
		get => _appsMonitored;
		set
		{
			if (_appsMonitored != value)
			{
				_appsMonitored = value;
			}
		}
	}
}

