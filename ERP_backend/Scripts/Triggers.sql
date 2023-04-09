go
CREATE TRIGGER PrviTriger
    ON [dbo].[stanje]
    for insert, update
    AS
    BEGIN
    SET NOCOUNT ON

	DECLARE @staro INT
	DECLARE @novo INT
	DECLARE @proizvod INT
	DECLARE @kolicina INT

	SET @staro = ISNULL((select deleted.kolicina from deleted), 0)
	set @novo = (select inserted.kolicina from inserted)
	set @proizvod = (select inserted.IDProizvod FROM inserted)
	set @kolicina = (select ukupnaKolicina from proizvod where IDProizvod = @proizvod)

	declare @razlika int = @staro - @novo

	update proizvod
	set ukupnaKolicina = ISNULL(@kolicina, 0) - @razlika
	where IDProizvod = @proizvod

    END



go
CREATE TRIGGER DrugiTriger
    ON [dbo].[stanje]
    AFTER delete
    AS
    BEGIN
    SET NOCOUNT ON

	declare @staro int = (select kolicina from deleted)

	declare @proizvod int = (select IDProizvod from deleted)
	declare	@kolicina int = (select ukupnaKolicina from proizvod where IDProizvod = @proizvod)


	update proizvod
	set ukupnaKolicina = @kolicina - @staro
	where IDProizvod = @proizvod

    END
