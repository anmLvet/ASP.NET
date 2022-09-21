         select p.name product_name
              , c.name category_name
           from products p
left outer join product_category pc on p.id = pc.productID
left outer join category c          on pc.categoryID = c.ID