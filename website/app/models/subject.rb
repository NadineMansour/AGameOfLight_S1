class Subject < ActiveRecord::Base
	#associations
	belongs_to :school_admin
	#validations
	validates :code, uniqueness: true
end
