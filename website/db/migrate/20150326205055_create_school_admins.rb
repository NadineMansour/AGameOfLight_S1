class CreateSchoolAdmins < ActiveRecord::Migration
  def change
	create_table :school_admins do |t|
		t.string :school
		t.boolean :verified, default: false	 
		t.timestamps null: false
	end
  end
end
