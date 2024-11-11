using System.Linq.Expressions;
using Microsoft.VisualBasic;

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
	Player player;



	public MainPage()
	{
		InitializeComponent();
		player=new Player(imgplayer);
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
	void CorrigeTamanhoCenario (double w, double h)
	{
		foreach (var A in HSLayer1.Children)
		(A as Image).WidthRequest = w;
		foreach (var A in HSLayer2.Children)
		(A as Image).WidthRequest = w;
		foreach (var A in HSLayer3.Children)
		(A as Image).WidthRequest = w;
		
		HSLayer1.WidthRequest = w ;
		HSLayer2.WidthRequest = w ;
		HSLayer3.WidthRequest = w ;
	}

	void GerenciaCenarios()
	{
		MoveCenario();
		GerenciaCenarios (HSLayer1);
		GerenciaCenarios (HSLayer2);
		GerenciaCenarios (HSLayer3);
		
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
	    while(!estaMorto)
	{
		GerenciaCenarios();
		player.Desenha();
		await Task.Delay(tempoEntreFrames);
	}
   }

    protected override void OnAppearing()
    {
        base.OnAppearing();
		Desenha();
    }
     
  

}

