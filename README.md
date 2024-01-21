# AfterlifeApp
Nasza strona internetowa to sklep z grami komputerowymi, napisana w języku C#. Aplikacja umożliwia przeglądanie, zamawianie i pobieranie gier komputerowych. Sklep posiada funkcje rejestracji kont użytkowników, logowania i wylogowania, składania zamówień przez poszczególnych klientów oraz przeglądanie historii zamówionych gier, bądź ich pobrania.


## Instrukcja
W celu uruchomienia strony internetowej sklepu należy uruchomić program Visual Studio. Następnie w konsoli menadżera pakietów NuGet należy wpisać komendę:
 - update-database

Kolejnym krokiem będzie wciśnięcie przycisku F5 lub naciśnięcie przycisku z wypełnioną zieloną strzałką na górze programu Visual Studio. Aplikacja poprosi o zaaplikowanie migracji i w tym celu należy podążać za wskazówkami podanymi na wyświetlonej stronie, prawdopodobnie będzie to wciśnięcie przycisku z napisem “Apply Migrations”. 

W celu uzyskania pełnej funkcjonalności należy zalogować się na konto administratora.
 - login: admin<span>@</span>example.com
 - password: zaq1@WSX
 
W celu dodania nowych produktów na stronie najlepiej trzymać się poniższej kolejności:
	
 1. Category, Bundle	
 2. Game	
 3. Order
