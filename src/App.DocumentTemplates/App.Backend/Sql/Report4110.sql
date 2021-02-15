select
	dt.Code,
	dt.Name,
	isnull(sas.CountAll,0) as CountAll,
	sum(isnull(sas.v1,0)) as gp,
	sum(isnull(sas.v2,0)) as cp,
	sum(isnull(sas.v3,0)) as kss,
	sum(isnull(sas.v4,0)) as etc
	from(
		select 
		dt.Code,
		d.Id,
		d.OriginDbRecordId,
		count(d.Id) over(partition by dt.Code) as CountAll,	
		case when dd.Value like '%Грудна порожнина%' then 1 end as v1,
		case when dd.Value like '%Черевна порожнина%' then 1 end as v2,
		case when dd.Value like '%Таз%'
					or dd.Value like '%Верхні кінцівки%' 
					or dd.Value like '%Нижні кінцівки%'
					or dd.Value like '%Шийний відділ хребта%'
					or dd.Value like '%Грудний відділ хребта%'
					or dd.Value like '%Поперековий відділ хребта%'
			then 1 end as v3,
		case when dd.Value like '%Голова%'
					or dd.Value like '%Шия%'
					or dd.Value like '%ДПН%'
		then 1 end as v4 
		from dss_Documents d
		right join DocTemplates dt on dt.Id = d.TemplateId
		join DocData dd on dd.DocumentId = d.Id and dd.Code = 'Dylyanka dosl'
		join Appointments a on a.Id = d.OriginDbRecordId
		where isnull(dd.Value,'') != '' 
			and dt.Code in (select Code from StudyTypes)
			and ((@DateFrom is null) or (convert(date,isnull(a.ActualStartDateTimeUtc, a.VisitStartDateTimeUtc)) >= convert(date,@DateFrom)))
			and ((@DateTo is null) or (convert(date,isnull(a.ActualStartDateTimeUtc, a.VisitStartDateTimeUtc)) <= convert(date,@DateTo)))
			and a.OwnerId = @OwnerId
			--тут должна быть дата, но ее нет
			--and @DateFrom is null or d.Date
	) sas
	right join DocTemplates dt on dt.Code = sas.Code
	where dt.Code in (select Code from StudyTypes)
	group by dt.Code,dt.Name, sas.CountAll