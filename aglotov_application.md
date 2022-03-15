# Глотов Алексей Александрович - "My Timemanager"

### Группа: 10 - МИ - 3
### Электронная почта: aaglotov_2@edu.hse.ru
### VK: https://vk.com/alexxios


**[ НАЗВАНИЕ ПРОЕКТА ]**

“My Timemanager”

**[ ПРОБЛЕМНОЕ ПОЛЕ ]**

В современном мире проблема тайм-менеджмента всегда была актуальна, однако после введения карантина и перехода большинства отраслей на дистанционный режим вопрос управления временем встал наиболее остро. К приложениям, направленным на решение этой проблемы, выдвинуты следующие требования: 1) возможность планирования задач, 2) разделение задач на категории и/или приоритеты, 3) работа на различных операционных системах. Однако, во всех имеющихся программных продуктах отсутствует, по моим данным: 1) создание событий с онлайн-конференциями, 2) наглядное представление распределения задач на приоритеты. Также многие продукты рассчитаны только на выполнение заданий на день, пользователь не получает напоминание о будущих целях. 

**[ ЗАКАЗЧИК / ПОТЕНЦИАЛЬНАЯ АУДИТОРИЯ ]**

Мой продукт может заинтересовать широкую аудиторию, так как включает в себя все функции планировщика задач, однако изначально он было ориентирован на конкретные группы пользователей:

* Учащиеся школ и вузов
* Преподаватели, учителя
* Офисные работники

**[ АППАРАТНЫЕ ТРЕБОВАНИЯ ]** 

Продукт разрабатывается под наиболее распространённые операционные системы в данный момент и к моменту сдачи должен существовать в виде версий для следующих конфигураций:

* iOS – версия 12 и выше, 100 Мб свободного места на гаджете
* Android – версия Nougat (Android 7) и выше, 120 Мб свободного места
* UWP, 120 Мб свободного места

**[ ФУНКЦИОНАЛЬНЫЕ ТРЕБОВАНИЯ ]**

Программный продукт будет предоставлять следующие возможности:

* Создание профилей пользователя
* Включение/отключение синхронизации с облачной БД
* Создание нового события/задания/дедлайна
* Уведомления о близжайших событиях/дедлайнах
* Добавление к событию ссылки на онлайн-конференцию (Zoom, MS Teams), запуск конференции прямо из приложения/по уведомлению
* Разделение заданий по объёму, создание подзадач с собственными сроками выполнения
* Уведомление пользователя о заданиях, которые необходимо завершить к концу текущего дня
* Распределение приоритетов заданий в матрице Эйзенхауэра
* Количество событий/заданий/дедлайнов, которые можно назначить за день планируется ограничить, однако возможно получение дополнительных за просмотр рекламы.

**[ ПОХОЖИЕ / АНАЛОГИЧНЫЕ ПРОДУКТЫ ]**

Анализ 3 программных продуктов, которые максимально приближены к заданному функционалу, показал, что:

* Tappsk: короткий пробный период (7 дней), высокая стоимость подписки (999 RUB в год), отсутствие разделения заданий на приоритеты, отсутствие версии для Windows 
*	RPLife: нет пробного периода, обязательная покупка (279.00 RUB на App Store), отсутствие разделения заданий на приоритеты, отсутствие версий для Android и Windows
* Todoist: в приложении есть только задания, не позволяет создавать события, отсутсвие версии для Windows

**[ ИНСТРУМЕНТЫ РАЗРАБОТКИ ]**

*	Xamarin Forms/ С# / Visual Studio – для разработки Android, iOS и UWP версий
* SQLite - для разработки БД
* Azure Functions / C# / Visual Studio Code - для разработки бэкэнд части (синхронизация с облачноым хранилищем)

**[ ЭТАПЫ РАЗРАБОТКИ ]**

*	Разработка пользовательских сценариев
*	Проектирование интерфейса
*	Реализация основного функционала приложения
*   Интеграция с рекламными сервисами (Google AdMob)
*	Запуск версий продукта на поддерживаемых ОС
*	Тестирование, отладка
*	Подготовка проекта к защите

**[ ВОЗМОЖНЫЕ РИСКИ ]**

*	Невозможность спроектировать удобный пользовательский интерфейс 
*	Невозможность наладить интеграцию с рекламными сервисами
*   Неверное представление об объёме работы, нехватка времени на реализацию всех элементов
