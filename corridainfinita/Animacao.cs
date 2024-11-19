namespace corridainfinita;
using FFImageLoading.Maui;


public class Animacao
{
    protected List<String> Animacao1 = new List<String>();
    protected List<String> Animacao2 = new List<String>();
    protected List<String> Animacao3 = new List<String>();
    protected bool loop = true;
    protected int AnimacaoAtiva = 1;
    bool parado = true;
    int frameAtual = 1;
    protected Image compImage;
    public Animacao(Image a)
    {
        compImage = a;
    }
    public void Stop()
    {
        parado = true;
    }
    public void Play()
    {
        parado = false;
    }
    public void SetAnimacaoAtiva(int a)
    {
        AnimacaoAtiva = a;
    }
    public void Desenha()
    {
        if (parado)
            return;
        string NomeArquivo = "";
        int TamanhoAnimacao = 0;
        if (AnimacaoAtiva == 1)
        {
            NomeArquivo = Animacao1[frameAtual];
            TamanhoAnimacao = Animacao1.Count;
        }
        else if (AnimacaoAtiva == 2)
        {
            NomeArquivo = Animacao2[frameAtual];
            TamanhoAnimacao = Animacao2.Count;
        }
        else if (AnimacaoAtiva == 3)
        {
            NomeArquivo = Animacao3[frameAtual];
            TamanhoAnimacao = Animacao3.Count;
        }
        compImage.Source = ImageSource.FromFile(NomeArquivo);
        frameAtual++;
        if (frameAtual >= TamanhoAnimacao)
        {
            if (loop)
                frameAtual = 0;
            else
            {
                parado = true;
                QuandoParar();
            }
        }

    }
    public virtual void QuandoParar()
    {

    }

}