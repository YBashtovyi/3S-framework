select
  expr.caption,
  expr.id,
  expr.code,
  expr.name,
  expr.prop_type_enum,
  expr.cotype_enum,
  expr.sort_order,
  (select name from enum_record  where  enum_type = 'PropType'  and code = expr.prop_type_enum) as prop_type,
  expr.group,
  expr.kind_enum,
  (select name from enum_record  where  enum_type = 'ObjectKind'  and code = expr.kind_enum) as kind
from ex_property expr
where expr.record_state<>4