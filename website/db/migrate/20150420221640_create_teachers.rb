class CreateTeachers < ActiveRecord::Migration
  def change
    create_table :teachers do |t|
    	t.string :teacher_name
    	t.boolean :verified, default: false
    	t.string :school
      t.timestamps null: false
    end
  end
end
