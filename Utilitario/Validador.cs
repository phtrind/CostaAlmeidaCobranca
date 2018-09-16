namespace Utilitario
{
    public class Validador
    {
        public static bool CpfIsValid(string aCpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            aCpf = aCpf.Trim().Replace(".", "").Replace("-", "");

            if (aCpf.Length != 11)
            {
                return false;
            }

            for (int j = 0; j < 10; j++)
            {
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == aCpf)
                {
                    return false;
                }
            }

            string tempCpf = aCpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }

            int resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            string digito = resto.ToString();

            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = digito + resto.ToString();

            return aCpf.EndsWith(digito);
        }

        public static bool EmailIsValid(string aEmail)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(aEmail);

                return addr.Address == aEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
