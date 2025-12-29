using Newtonsoft.Json;
using Newtonsoft;
using System.Net.Http.Json;

namespace VSLangNameGenerator
{
    public class Program
    {
        static Dictionary<string, string> Materials {  get; set; }
        static List<string> BlockTypeFileName { get; set; } = new List<string>();
        static void Main(string[] args)
        {
            bool resultLoadMaterials = LoadMaterials();
            Console.WriteLine("Напиши путь до папки с файлами blocktypes.");
            string blocktypeFolderPath = Console.ReadLine();
            string folderName = Path.GetFileName(blocktypeFolderPath);
            var jsonFiles = Directory.GetFiles(blocktypeFolderPath, "*.json");
            if (jsonFiles.Length > 0)
            {
                Console.WriteLine($"В {folderName} нашел следующие файлы.");

                foreach (var file in jsonFiles)
                {
                    if (file.Contains("dummy")) continue;
                    Console.WriteLine($"{folderName}\\"+ Path.GetFileName(file));
                    BlockTypeFileName.Add(Path.GetFileName(file));
                }
            }
            else
            {
                Console.WriteLine("Файлы формата .json не найдены в указанной папке.");
            }
            Console.WriteLine("________________________________\n");
            Console.WriteLine($"Пройдемся по всем файлам.");
            Console.WriteLine("________________________________\n");
            foreach (var name in BlockTypeFileName)
            {
                Console.WriteLine($"{name} какое имя для этого блока установить?");
                string blockName = Console.ReadLine();
                Console.WriteLine("\n");
                bool result = BlockNameGenerator($"{blocktypeFolderPath}\\{name}", blockName);
                Console.WriteLine("________________________________\n");

            }
            Console.WriteLine("Well Done.");
            var andLine = Console.ReadLine();
        }


        static bool BlockNameGenerator(string msg, string blockName)
        {
            var file = File.ReadAllText(msg);
            var rootObject = JsonConvert.DeserializeObject<RootObject>(file);

            if(rootObject.VariantGroups.Count == 1)
            {
                string fullFileName = $"\"block-{rootObject.Code}-north\": \"{blockName}\"";
                Console.WriteLine(fullFileName);
                SaveFile(fullFileName);
            }
            else if(rootObject.VariantGroups.Count > 1)
            {
                foreach (var group in rootObject.VariantGroups)
                {
                    if (group.States != null && group.States.Count > 0)
                    {

                        foreach (string state in group.States)
                        {
                            if (state.Contains("dummy")) continue;
                            string materialName = state;
                            if (Materials.ContainsKey(state)) materialName = Materials[state];
                            string fullFileName = $"\"block-{rootObject.Code}-{state}-north\": \"{blockName} ({materialName})\"";
                            Console.WriteLine(fullFileName);
                            SaveFile(fullFileName);
                        }
                    }
                }
            }    
            
            return true;
        }

        static bool SaveFile(string content)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"lang.txt");
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
            }

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(content);
            }

            return true;
        }

        static bool LoadMaterials()
        {
            Materials = new Dictionary<string, string>();

            Materials.Add("acacia", "акация");
            Materials.Add("birch", "береза");
            Materials.Add("kapok", "капок");
            Materials.Add("maple", "клён");
            Materials.Add("oak", "дуб");
            Materials.Add("pine", "сосна");
            Materials.Add("baldcypress", "болотный кипорис");
            Materials.Add("larch", "лиственница");
            Materials.Add("redwood", "секвойя");
            Materials.Add("ebony", "чёрное дерево");
            Materials.Add("walnut", "грецкий орех");
            Materials.Add("purpleheart", "пурпурное дерево");

            Materials.Add("bismuth", "висмут");
            Materials.Add("bismuthbronze", "бисмутовая бронза");
            Materials.Add("blackbronze", "чёрная бронза");
            Materials.Add("brass", "латунь");
            Materials.Add("chromium", "хром");
            Materials.Add("copper", "медь");
            Materials.Add("electrum", "электрум");
            Materials.Add("gold", "золото");
            Materials.Add("iron", "железо");
            Materials.Add("lead", "свинец");
            Materials.Add("meteoriciron", "метеоритное железо");
            Materials.Add("molybdochalkos", "молибдохалк");
            Materials.Add("nickel", "никель");
            Materials.Add("platinum", "платина");
            Materials.Add("silver", "серебро");
            Materials.Add("stainlesssteel", "нержавеющая сталь");
            Materials.Add("steel", "сталь");
            Materials.Add("tin", "олово");
            Materials.Add("tinbronze", "оловянная бронза");
            Materials.Add("titanium", "титан");
            Materials.Add("uranium", "уран");
            Materials.Add("zinc", "цинк");
            return true;
        }
    }
}
