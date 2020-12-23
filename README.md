Видюшка в файлах репозитория)

Многие методы стали асинхронными. Использованием стиль TAP.
Добавил асинхронное поведение в:

 -Data Access Layer.
 
 -Service Layer.
 
 -операции чтения/записи файлов.
 
 -операции с сетевой передачей данных.
 
 ```C#
 public async Task<Customer> Get(int id)
        {
            return await Task.Run(() => {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();
                    var command = new SqlCommand("GetCustomerByID", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Transaction = transaction;

                    try
                    {
                        command.Parameters.Add(new SqlParameter("@ID", id));
                        var reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var customer = new Customer
                                {
                                    Id = id,
                                    LastName = reader.GetString(0),
                                    FirstName = reader.GetString(1),
                                    EmailAddress = reader.GetString(2),
                                    Phone = reader.GetString(3),
                                    City = reader.GetString(4),
                                    Address = reader.GetString(5),
                                    PostalCode = reader.GetString(6)
                                };
                                return customer;
                            }
                        }
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        ILogger logger = new Logger();
                        logger.WriteErrorToDB(e.Message);
                    }
                }
                throw new KeyNotFoundException();
            });
        }
```


Каждую итерацию работы сервиса запускал в отдельном потоке из ThreadPool.
```C#
ThreadPool.QueueUserWorkItem(async state =>
            {
                var repositories = new UnitOfWork();
                OrderService orderService = new OrderService(repositories);
                var ordersInfo = await orderService.GetOrdersInfo();
                IXmlGeneratorService<OrderInfo> xmlGenerator = new XmlGeneratorService<OrderInfo>();
                await xmlGenerator.GenerateXml(OptionsLib.PathOptions.SourceDirectory, ordersInfo);
            });
```



Старое:

bd AdventureWorksLT2019

В решении 6 проектов: 
1. Models. new

   -Модели из БД.

2. FileManager

3. ConfigurationManager

4. DataAccess. new

   -Нижний уровень. Работа с базой данных. Заполнение моделей через единый репозиторий. Логгирование исключений в БД с ошибками.

5. ServiceLayer. new

   -Средний уровень. Бизнес логика. Подготовка модели для передачи выше.

6. DataManager. new

   -Веръний уровень. Создание XML. Транспортировка данных.

![Screenshot](Screenshots/Screenshot_4.png)

Хранимые процедуры в БД:

![Screenshot](Screenshots/Screenshot_5.png)

Пример процедуры получения покупателя по id:

![Screenshot](Screenshots/Screenshot_6.png)

Имеются БД и процедуры для хранения логов с такой таблицей:

![Screenshot](Screenshots/Screenshot_7.png)


