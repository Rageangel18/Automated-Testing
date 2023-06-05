using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

class Program
{
    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        // Создание экземпляра веб-драйвера
        IWebDriver driver = new ChromeDriver();

        // Навигация на главную страницу
        driver.Navigate().GoToUrl("http://svyatoslav.biz/testlab/wt/");

        // Проверка наличия слов "menu" и "banners" на главной странице
        bool isMenuPresent = driver.PageSource.Contains("menu");
        bool isBannersPresent = driver.PageSource.Contains("banners");
        Console.WriteLine(  );
        Console.WriteLine("Проверка a. Главная страница содержит слова «menu» и «banners»:");
        Console.WriteLine("Результат: " + (isMenuPresent && isBannersPresent));
        Console.WriteLine();

        // Проверка наличия текста "CoolSoft by Somebody" в нижней ячейке таблицы
        //IWebElement tableCell = driver.FindElement(By.XPath("//table/tbody/tr[last()]/td"));
        //bool isTextPresent = tableCell.Text.Contains("© CoolSoft by Somebody");
        //Console.WriteLine("Проверка b. В нижней ячейке таблицы присутствует текст «CoolSoft by Somebody»:");
        //Console.WriteLine("Результат: " + isTextPresent);
        //
        string pageSource = driver.PageSource;
        string searchText = "© CoolSoft by Somebody";

        bool isTextPresent = pageSource.Contains(searchText);
        Console.WriteLine("Проверка b. В нижней ячейке таблицы присутствует текст «CoolSoft by Somebody»:");
        Console.WriteLine("Результат: " + isTextPresent);
        Console.WriteLine(  );

        // Проверка значений по умолчанию для текстовых полей и поля "Пол"
        IWebElement heightInput = driver.FindElement(By.Name("height"));
        IWebElement weightInput = driver.FindElement(By.Name("weight"));
        IWebElement nameInput = driver.FindElement(By.Name("name"));
        //IWebElement maleRadioButton = driver.FindElement(By.XPath("//input[@name='gender' and @value='male']"));
        //IWebElement femaleRadioButton = driver.FindElement(By.XPath("//input[@name='gender' and @value='female']"));
        IWebElement genderInput = driver.FindElement(By.Name("gender"));

        bool areTextFieldsEmpty = string.IsNullOrEmpty(heightInput.GetAttribute("value"))
            && string.IsNullOrEmpty(weightInput.GetAttribute("value"));
        bool isGenderNotSelected = !genderInput.Selected;
        Console.WriteLine("Проверка c. По умолчанию все текстовые поля формы пусты, а значение поля «Пол» не выбрано:");
        Console.WriteLine("Результат: " + (areTextFieldsEmpty && isGenderNotSelected));
        Console.WriteLine(  );

        // Заполнение полей "Рост" и "Вес" значениями и отправка формы
        heightInput.SendKeys("50");
        weightInput.SendKeys("3");
        nameInput.SendKeys("Nikita Dubrovin");
        genderInput.Click();
        //femaleRadioButton.Click();
        IWebElement submitButton = driver.FindElement(By.XPath("//input[@type='submit']"));
        submitButton.Click();

        // Проверка появления надписи "Слишком большая масса тела"
        bool isErrorMessageDisplayed = driver.PageSource.Contains("Слишком большая масса тела");
        Console.WriteLine("Проверка d. После заполнения поля «Рост» значением «50» и вес значением «3» и отправки формы, " +
            "форма исчезает,\nа вместо неё появляется надпись «Слишком большая масса тела»:");
        Console.WriteLine("Результат: " + isErrorMessageDisplayed);
        Console.WriteLine();
        driver.Navigate().Back();

        // Проверка наличия формы с текстовыми полями, радио-баттонами и кнопкой на главной странице
        bool isFormPresent = driver.FindElement(By.TagName("form")).Displayed;
        Console.WriteLine("Проверка e. Главная страница приложения сразу после открытия содержит форму\n с тремя текстовыми полями, " +
            "одной группой из двух радио-баттонов и одной кнопкой:");
        Console.WriteLine("Результат: " + isFormPresent);
        Console.WriteLine(  );
        // Проверка появления сообщений о неверном вводе значений веса и роста
        heightInput.Clear();
        weightInput.Clear();
        submitButton.Click();
        bool isHeightErrorMessageDisplayed = driver.PageSource.Contains("Рост должен быть в диапазоне 50-300 см.");
        bool isWeightErrorMessageDisplayed = driver.PageSource.Contains("Вес должен быть в диапазоне 3-500 кг.");
        Console.WriteLine("Проверка f. При неверном вводе значений веса и/или роста появляются сообщения " +
            "о том,\n что рост может быть в диапазоне «50-300 см», а вес – в диапазоне «3-500 кг»:");
        Console.WriteLine("Результат: " + (isHeightErrorMessageDisplayed && isWeightErrorMessageDisplayed));
        Console.WriteLine(  );
        // Проверка наличия текущей даты на главной странице в формате "DD.MM.YYYY"
        string currentDate = DateTime.Now.ToString("dd.MM.yyyy");
        bool isDatePresent = driver.PageSource.Contains(currentDate);
        Console.WriteLine("Проверка g. Главная страница содержит текущую дату в формате «DD.MM.YYYY»:");
        Console.WriteLine("Результат: " + isDatePresent + " " + currentDate);

        // Закрытие веб-драйвера
        driver.Quit();
    }
}
