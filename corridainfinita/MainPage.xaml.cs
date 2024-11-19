using System.Linq.Expressions;
using Microsoft.VisualBasic;
using FFImageLoading.Maui;



namespace corridainfinita;

public partial class MainPage : ContentPage
{
	bool estaMorto = false;
	bool estaPulando = false;
	const int tempoEntreFrames = 25;
	int velocidade1 = 0;
	int velocidade2 = 0;
	int velocidade3 = 0;
	int velocidade = 0;
	int larguraJanela = 0;
	int alturaJanela = 0;
	const int forcaGravidade = 10;
	bool estanoChao = true;
	bool estanoAr = false;
	int tempoPulando = 0;
	int temponoAr = 0;
	const int forcaPulo = 10;
	const int maxTempoPulando = 10;
	const int maxTemponoAr = 5;
	Player player;



	public MainPage()
	{
		InitializeComponent();
		player = new Player(imgplayer);
		player.Run();
	}

	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		CorrigeTamanhoCenario(w, h);
		CalculaVelocidade(w);
	}
	void CalculaVelocidade(double w)
	{
		velocidade1 = (int)(w * 0.003);
		velocidade2 = (int)(w * 0.004);
		velocidade3 = (int)(w * 0.008);
		velocidade = (int)(w * 0.01);

	}
	void CorrigeTamanhoCenario(double w, double h)
	{
		foreach (var A in HSLayer1.Children)
			(A as Image).WidthRequest = w;
		foreach (var A in HSLayer2.Children)
			(A as Image).WidthRequest = w;
		foreach (var A in HSLayer3.Children)
			(A as Image).WidthRequest = w;

		HSLayer1.WidthRequest = w;
		HSLayer2.WidthRequest = w;
		HSLayer3.WidthRequest = w;
	}

	void GerenciaCenarios()
	{
		MoveCenario();
		GerenciaCenarios(HSLayer1);
		GerenciaCenarios(HSLayer2);
		GerenciaCenarios(HSLayer3);

	}
	void MoveCenario()
	{
		HSLayer1.TranslationX -= velocidade1;
		HSLayer2.TranslationX -= velocidade2;
		HSLayer3.TranslationX -= velocidade3;
	}
	void GerenciaCenarios(HorizontalStackLayout horizontalStackLayout)
	{
		var view = (horizontalStackLayout.Children.First() as Image);
		if (view.WidthRequest + horizontalStackLayout.TranslationX < 0)
		{
			horizontalStackLayout.Children.Remove(view);
			horizontalStackLayout.Children.Add(view);
			horizontalStackLayout.TranslationX = view.TranslationX;
		}
	}

	async Task Desenha()
	{
		while (!estaMorto)
		{
			GerenciaCenarios();
			if (!estaPulando && !estanoAr)

			{
				AplicaGravidade();
				player.Desenha();
				
			}
			else
				AplicaPulo();
			await Task.Delay(tempoEntreFrames);
		}
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		Desenha();
	}
    

	void AplicaGravidade()
	{
		if (player.GetY() < 0)
			player.MoveY(forcaGravidade);
		else if (player.GetY() >= 0)
		{
			player.SetY(0);
			estanoChao = true;
		}
	}
	void AplicaPulo()
	{
		estanoChao = false;
		if (estaPulando && tempoPulando >= maxTempoPulando)
		{
			estaPulando = false;
			estanoAr = true;
			temponoAr = 0;
		}
		else if (estanoAr && temponoAr >= maxTemponoAr)
		{
			estaPulando = false;
			estanoAr = false;
			tempoPulando = 0;
			temponoAr = 0;
		}
		else if (estaPulando && tempoPulando < maxTempoPulando)
		{
			player.MoveY(-forcaPulo);
			tempoPulando++;
		}
		else if (estanoAr)
			temponoAr++;
	}

	void OnGridClicked(object o, TappedEventArgs a)
	{
		if (estanoChao)
			estaPulando = true;
	}


}

