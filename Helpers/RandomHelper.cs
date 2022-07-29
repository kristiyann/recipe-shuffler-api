using System.Security.Cryptography;

namespace recipe_shuffler.Helpers
{
    public static class RandomHelper
    {
        public static void ShuffleCollection<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new ();

            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
