class CreateSchoolAdmins < ActiveRecord::Migration
  def change
    create_table :school_admins do |t|

      t.timestamps null: false
    end
  end
end
