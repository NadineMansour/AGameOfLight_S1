ActiveAdmin.register SchoolAdmin do
 
  #Display email , and school of those who request to be school admins and haven't yet been verified.
  
index do
  
  if current_school_admin.verified== false
  column :email
  column :school
 
  end
  end
end
