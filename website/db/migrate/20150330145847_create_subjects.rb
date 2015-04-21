class CreateSubjects < ActiveRecord::Migration
  def change
    create_table :subjects do |t|
      t.string :name
      t.string :code
      t.belongs_to :school_admin
      t.string :grade
      t.string :school
      t.timestamps null: false
    end
  end
end
